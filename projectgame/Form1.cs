using System;
using System.Data.SqlTypes;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace ProjectGame
{
    public partial class Form1 : Form
    {
        // Dynamic objects
        List<PictureBox> coinItems = new List<PictureBox>();
        List<PictureBox> hpItems = new List<PictureBox>();
        List<PictureBox> enemyBullets = new List<PictureBox>();
        List<PictureBox> bullets = new List<PictureBox>();
        List<Enemy> enemies = new List<Enemy>();
        Random rnd = new Random();
        Random enemyRnd = new Random();
        ProgressBar bossHPBar = null;

        // Player/Game
        int money = 0;
        string moneyFile = "money_save.txt";
        int gameMoney = 0;
        int playerHP = 100;
        int score = 0;
        int playerSpeed = 15;
        int bulletSpeed = 18;
        int enemySpeed = 3;
        int enemyBulletSpeed = 8;
        int itemFallSpeed = 5; // ความเร็วตกของไอเทมทั้งหมด
        int spawnRate = 60;
        int shootCounter = 0;
        int currentWave = 1;
        int maxWave = 50;
        int enemiesToSpawn = 3;
        int enemiesSpawned = 0;
        int enemiesKilled = 0;
        int bulletLevel = 1; // กระสุนเริ่มต้น
        int maxBulletLevel = 5;

        bool isBossAlive = false;
        bool goLeft = false;
        bool goRight = false;
        bool shooting = false;
        int spawnCounter = 0;

        // Upgrade
        int damageBonus = 0;
        int hpBonus = 0;

        // === Enemy class ===
        public class Enemy
        {
            public PictureBox sprite;
            public int HP;
            public bool isBoss;
            public Enemy(PictureBox pic, int hp, bool boss = false)
            {
                sprite = pic;
                HP = hp;
                isBoss = boss;
            }
        }

        public Form1()
        {
            InitializeComponent();
            hpBar.Visible = false;
            lblScore.Visible = false;
            player.Visible = false;
            panelGameOver.Visible = false;
            panelShop.Visible = false;
            panelStartMenu.Visible = true;
            panelStartMenu.BringToFront();

            this.BackgroundImage = Image.FromFile("ava.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            player.BackColor = Color.Transparent;
            player.BringToFront();

            gameTimer.Stop();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            LoadMoney();
            lblMoney.Text = "Money: " + money;
        }

        void SaveMoney()
        {
            File.WriteAllText(moneyFile, money.ToString());
        }

        void LoadMoney()
        {
            if (File.Exists(moneyFile))
            {
                string txt = File.ReadAllText(moneyFile);
                if (int.TryParse(txt, out int m))
                    money = m;
                else
                    money = 0;
            }
            else
            {
                money = 0;
                File.WriteAllText(moneyFile, "0");
            }
        }

        private void RemoveBossHPBar()
        {
            if (bossHPBar != null)
            {
                if (this.Controls.Contains(bossHPBar))
                    this.Controls.Remove(bossHPBar);

                try { bossHPBar.Dispose(); } catch { }
                bossHPBar = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            base.OnPaint(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                goLeft = true;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                goRight = true;

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                Shoot();
        }

        private void Shoot()
        {
            int bulletCount = bulletLevel; // จำนวนกระสุนตาม level
            int spacing = 15; // ระยะห่างกระสุน

            for (int i = 0; i < bulletCount; i++)
            {
                PictureBox bullet = new PictureBox();
                bullet.Size = new Size(5, 20);
                bullet.BackColor = Color.Yellow;
                bullet.BringToFront();

                // จัดตำแหน่งกระสุนแต่ละอัน ให้อยู่รอบตัวผู้เล่น
                int offset = 0;
                if (bulletCount > 1)
                {
                    offset = (i - bulletCount / 2) * spacing;
                    if (bulletCount % 2 == 0) offset += spacing / 2;
                }

                bullet.Location = new Point(player.Left + player.Width / 2 - bullet.Width / 2 + offset, player.Top - bullet.Height);
                bullets.Add(bullet);
                this.Controls.Add(bullet);
            }
        }

        private void DropMultiShotItem(int x, int y)
        {
            PictureBox multiShot = new PictureBox();
            multiShot.Size = new Size(30, 30);
            multiShot.SizeMode = PictureBoxSizeMode.StretchImage;
            multiShot.Image = Image.FromFile("multishot.png"); // ใส่ภาพไอเท็ม
            multiShot.Tag = "multishot"; // <-- ใช้ PictureBox.Tag แทน Image.Tag
            multiShot.BackColor = Color.Transparent;
            multiShot.BringToFront();
            multiShot.Location = new Point(x, y);

            hpItems.Add(multiShot); // ใช้ไอเท็มเดียวกับ hpItems หรือสร้าง list ใหม่ก็ได้
            this.Controls.Add(multiShot);
        }

        private void DropHPItem(int x, int y)
        {
            PictureBox hp = new PictureBox();
            hp.Size = new Size(30, 30);
            hp.SizeMode = PictureBoxSizeMode.StretchImage;
            hp.Image = Image.FromFile("medic.png");    // ใส่ภาพไอเท็มของคุณ
            hp.Tag = "hp"; // <-- ใช้ PictureBox.Tag แทน Image.Tag
            hp.BackColor = Color.Transparent;
            hp.BringToFront();
            hp.Location = new Point(x, y);

            hpItems.Add(hp);
            this.Controls.Add(hp);
        }

        private void SpawnEnemy()
        {
            PictureBox enemyPic = new PictureBox();
            enemyPic.Size = new Size(60, 60);
            enemyPic.SizeMode = PictureBoxSizeMode.StretchImage;
            enemyPic.Image = Image.FromFile("enemybg.png");
            enemyPic.BackColor = Color.Transparent;
            enemyPic.BringToFront();
            enemyPic.Location = new Point(rnd.Next(0, Math.Max(1, this.ClientSize.Width - 40)), -40);

            int enemyHP = 30;  // เลือดศัตรู (ปรับได้)

            Enemy newEnemy = new Enemy(enemyPic, enemyHP);

            enemies.Add(newEnemy);
            this.Controls.Add(enemyPic);
        }

        private void EnemyShoot(PictureBox enemy)
        {
            PictureBox bullet = new PictureBox();
            bullet.Size = new Size(5, 15);
            bullet.BackColor = Color.Orange;   // สีลูกกระสุนศัตรู
            bullet.BringToFront();
            bullet.Location = new Point(enemy.Left + enemy.Width / 2 - 2, enemy.Bottom);

            enemyBullets.Add(bullet);
            this.Controls.Add(bullet);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                goLeft = false;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                goRight = false;
        }

        private void ResetGame()
        {
            RemoveAllObjects();
            // รีเซตคะแนน
            score = 0;
            lblScore.Text = "Score: 0";
            // รีเซตตัวนับ spawn
            spawnCounter = 0;
            // ปิดปุ่ม restart และ start ตอนเล่นเกม
            btnRestart.Visible = false;
            btnstart.Visible = true;
            // รีเซตค่า Player แต่ *ไม่รีเซตเงิน*
            playerHP = 100;
            hpBar.Value = 100;
            // อัปเดตเงินแสดงในเกม
            gameMoney = 0;
            panelGameOver.Visible = false;
            lblMoney.Text = "Money: 0";
            // รีเซตเลเวลกระสุนกลับไปเริ่มต้น
            bulletLevel = 1;
            // รีเซ็ตโบนัสชั่วคราว
            damageBonus = 0;
            hpBonus = 0;

            // ถ้ามีบาร์บอส ควรลบ
            RemoveBossHPBar();
            isBossAlive = false;
        }

        private void RemoveAllObjects()
        {
            foreach (var b in bullets) if (this.Controls.Contains(b)) this.Controls.Remove(b);
            foreach (var eb in enemyBullets) if (this.Controls.Contains(eb)) this.Controls.Remove(eb);
            foreach (var en in enemies) if (this.Controls.Contains(en.sprite)) this.Controls.Remove(en.sprite);
            foreach (var hp in hpItems) if (this.Controls.Contains(hp)) this.Controls.Remove(hp);
            foreach (var c in coinItems) if (this.Controls.Contains(c)) this.Controls.Remove(c);

            bullets.Clear();
            enemyBullets.Clear();
            enemies.Clear();
            hpItems.Clear();
            coinItems.Clear();

            RemoveBossHPBar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMoney();
            lblMoneyMenu.Text = "Money: " + money;   // เอาไปโชว์หน้าเมนู
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            hpBar.Visible = true;
            lblScore.Visible = true;
            player.Visible = true;

            MovePlayer();
            UpdateBullets();
            spawnCounter++;
            if (!isBossAlive)
            {
                if (spawnCounter >= spawnRate && enemiesSpawned < enemiesToSpawn)
                {
                    SpawnEnemy();
                    enemiesSpawned++;
                    spawnCounter = 0;
                }
            }
            UpdateEnemies();
            CheckBulletHitEnemies();
            UpdateEnemyBullets();
            UpdateHPItems();
            UpdateCoins();
            UpdateUI();
        }

        private void MovePlayer()
        {
            if (goLeft && player.Left > 0) player.Left -= playerSpeed;
            if (goRight && player.Right < ClientSize.Width) player.Left += playerSpeed;

            if (shooting)
            {
                shootCounter++;
                if (shootCounter % 5 == 0) Shoot();
            }
        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Top -= bulletSpeed;
                if (bullets[i].Top < -20)
                {
                    if (this.Controls.Contains(bullets[i])) this.Controls.Remove(bullets[i]);
                    bullets.RemoveAt(i);
                }
            }
        }

        private void UpdateEnemies()
        {
            // สำคัญ: ไม่ลบ enemy ภายใน loop นี้เมื่อเจอการตายจากที่อื่น
            // การลบให้ทำจาก RemoveEnemySafe (ที่ตรวจสอบก่อน)
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = enemies[i];
                PictureBox en = enemy.sprite;

                if (enemy.isBoss)
                {
                    // ถ้าบอสตายแล้ว ให้ข้ามการอัปเดต (ลบจริง ๆ จะทำจาก CheckBulletHitEnemies)
                    if (enemy.HP <= 0) continue;

                    // บอสเคลื่อนที่ซ้ายขวา
                    if (en.Tag == null)
                        en.Tag = 1; // 1 = ขวา, -1 = ซ้าย

                    int dir = (int)en.Tag;
                    en.Left += dir * 5;

                    if (en.Left <= 0) dir = 1;
                    if (en.Right >= Math.Max(1, this.ClientSize.Width)) dir = -1;
                    en.Tag = dir;

                    // บอสยิงลูกกระสุน
                    if (enemyRnd.Next(0, 50) == 1) EnemyShoot(en);

                    // อัปเดต HP Bar (ถ้ามี)
                    if (bossHPBar != null)
                        bossHPBar.Value = Math.Max(0, Math.Min(enemy.HP, bossHPBar.Maximum));
                }
                else
                {
                    // ศัตรูธรรมดา
                    en.Top += enemySpeed;

                    if (enemyRnd.Next(0, 50) == 1) EnemyShoot(en);

                    if (en.Top > ClientSize.Height)
                    {
                        RemoveEnemySafe(enemy);
                        continue;
                    }
                    if (en.Bounds.IntersectsWith(player.Bounds))
                    {
                        GameOver();
                        return;
                    }
                }
            }
        }

        private void CheckBulletHitEnemies()
        {
            // loop bullets -> enemies: แต่ต้องระวังการลบซ้ำ
            for (int ei = enemies.Count - 1; ei >= 0; ei--)
            {
                Enemy enemy = enemies[ei];

                for (int bi = bullets.Count - 1; bi >= 0; bi--)
                {
                    var b = bullets[bi];
                    if (b == null || enemy == null) continue;
                    if (b.Bounds.IntersectsWith(enemy.sprite.Bounds))
                    {
                        // ลบกระสุน
                        if (this.Controls.Contains(b)) this.Controls.Remove(b);
                        bullets.RemoveAt(bi);

                        // ลด HP
                        enemy.HP -= 10 + damageBonus;

                        // ถ้าเป็นบอส อัปเดตบาร์ และลบถ้าตาย
                        if (enemy.isBoss)
                        {
                            if (bossHPBar != null)
                                bossHPBar.Value = Math.Max(0, Math.Min(enemy.HP, bossHPBar.Maximum));

                            if (enemy.HP <= 0)
                            {
                                // ลบบาร์บอส
                                RemoveBossHPBar();
                                isBossAlive = false;

                                // ลบตัวบอสแบบปลอดภัย
                                RemoveEnemySafe(enemy);

                                // ไปเวฟต่อ
                                currentWave++;
                                StartWave();
                            }

                            // บอสถูกกระสุน 1 นัดพอแล้ว ออกจาก loop ลูกกระสุน
                            break;
                        }
                        else
                        {
                            // ศัตรูปกติ
                            if (enemy.HP <= 0)
                            {
                                RemoveEnemySafe(enemy);
                                // เมื่อฆ่าศัตรูแล้ว ออกจาก loop bullets (เพราะ enemy ถูกลบ)
                                break;
                            }
                        }
                    }
                }
            }
        }

        // ปลอดภัย: ลบ enemy โดยตรวจสอบว่ามีอยู่จริงใน list ก่อน
        private void RemoveEnemySafe(Enemy enemy)
        {
            if (enemy == null) return;

            // ถ้ามี sprite อยู่บน Form ให้ลบก่อนอ่านตำแหน่งเพื่อ drop item
            int dropX = enemy.sprite != null ? enemy.sprite.Left : 0;
            int dropY = enemy.sprite != null ? enemy.sprite.Top : 0;

            if (this.Controls.Contains(enemy.sprite)) this.Controls.Remove(enemy.sprite);

            // ลบจาก list ด้วยการอ้างอิง (ไม่ใช้ index ที่อาจเก่าแล้ว)
            if (enemies.Contains(enemy))
                enemies.Remove(enemy);
            else
                return; // ถูกลบไปแล้ว

            // เพิ่มคะแนน
            score++;
            lblScore.Text = "Score: " + score;

            // ดรอปไอเท็ม เฉพาะศัตรูปกติ
            if (!enemy.isBoss)
            {
                if (rnd.Next(0, 100) < 30) DropHPItem(dropX, dropY);
                if (rnd.Next(0, 100) < 15) DropMultiShotItem(dropX, dropY);
                if (rnd.Next(0, 100) < 40) DropCoin(dropX, dropY);
            }

            enemiesKilled++;

            // ถ้าเป็นศัตรูธรรมดาครบ ให้ไปเวฟต่อ
            if (!isBossAlive && enemiesKilled >= enemiesToSpawn)
            {
                currentWave++;
                StartWave();
            }
        }

        private void UpdateEnemyBullets()
        {
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                PictureBox eb = enemyBullets[i];
                eb.Top += enemyBulletSpeed;
                if (eb.Top > ClientSize.Height) { if (this.Controls.Contains(eb)) this.Controls.Remove(eb); enemyBullets.RemoveAt(i); continue; }
                if (eb.Bounds.IntersectsWith(player.Bounds))
                {
                    if (this.Controls.Contains(eb)) this.Controls.Remove(eb);
                    enemyBullets.RemoveAt(i);
                    playerHP -= 20;
                    if (playerHP <= 0) { GameOver(); return; }
                    hpBar.Value = Math.Max(0, playerHP);
                }
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show("Game Over!");

            money += gameMoney;
            SaveMoney();

            panelStartMenu.Visible = false;
            panelShop.Visible = false;
            panelGameOver.Visible = true;
            panelGameOver.BringToFront();

            hpBar.Visible = false;
            lblScore.Visible = false;
            lblMoney.Visible = false;

            goLeft = false;
            goRight = false;
            shooting = false;
        }

        private void UpdateHPItems()
        {
            for (int i = hpItems.Count - 1; i >= 0; i--)
            {
                PictureBox item = hpItems[i];
                item.Top += itemFallSpeed; // ใช้ itemFallSpeed เดียวกันทุกชิ้น

                // ออกนอกจอ → ลบ
                if (item.Top > ClientSize.Height)
                {
                    if (this.Controls.Contains(item)) this.Controls.Remove(item);
                    hpItems.RemoveAt(i);
                    continue;
                }

                // ชนผู้เล่น
                if (item.Bounds.IntersectsWith(player.Bounds))
                {
                    string type = (item.Tag != null) ? item.Tag.ToString() : "";

                    if (this.Controls.Contains(item)) this.Controls.Remove(item);
                    hpItems.RemoveAt(i);

                    // ถ้าเป็นไอเท็มเพิ่ม HP
                    if (type == "hp")
                    {
                        playerHP = Math.Min(playerHP + 30, 100 + hpBonus);
                        hpBar.Maximum = 100 + hpBonus;
                        hpBar.Value = playerHP;
                    }
                    // ถ้าเป็นไอเท็มยิงหลายทิศ (multishot)
                    else if (type == "multishot")
                    {
                        if (bulletLevel < maxBulletLevel) bulletLevel++;
                    }
                }
            }
        }

        private void UpdateCoins()
        {
            for (int i = coinItems.Count - 1; i >= 0; i--)
            {
                PictureBox coin = coinItems[i];
                coin.Top += itemFallSpeed;
                if (coin.Top > ClientSize.Height) { if (this.Controls.Contains(coin)) this.Controls.Remove(coin); coinItems.RemoveAt(i); continue; }
                if (coin.Bounds.IntersectsWith(player.Bounds))
                {
                    gameMoney += 10;
                    lblMoney.Text = "Money: " + gameMoney;

                    SaveMoney();
                    if (this.Controls.Contains(coin)) this.Controls.Remove(coin);
                    coinItems.RemoveAt(i);
                }
            }
        }

        private void UpdateUI()
        {
            lblScore.Text = "Score: " + score;
            hpBar.Value = Math.Max(0, Math.Min(hpBar.Maximum, playerHP));
            lblMoney.Text = "Money: " + gameMoney;
            lblMoneyMenu.Text = "Money: " + money;
        }
        private void DropCoin(int x, int y)
        {
            PictureBox coin = new PictureBox();
            coin.Size = new Size(25, 25);
            coin.SizeMode = PictureBoxSizeMode.StretchImage;
            coin.Image = Image.FromFile("coin.png");
            coin.Tag = "coin";
            coin.BackColor = Color.Transparent;
            coin.BringToFront();
            coin.Location = new Point(x, y);

            coinItems.Add(coin);
            this.Controls.Add(coin);
        }

        private void StartWave()
        {
            enemiesSpawned = 0;
            enemiesKilled = 0;

            // ถ้าเป็น Wave ของบอส
            if (currentWave % 10 == 0)
            {
                enemiesToSpawn = 1;     // มีแค่บอสตัวเดียว
                isBossAlive = true;
                SpawnBoss();
                lblWave.Text = "BOSS WAVE " + currentWave;
                return;
            }

            spawnRate = 60 - (currentWave * 2);  // Wave สูงขึ้น ศัตรูเกิดเร็วขึ้น
            if (spawnRate < 10) spawnRate = 10;  // ไม่ให้เร็วเกินไป
            enemiesToSpawn = Math.Max(1, currentWave);  // ปรับเป็นขั้นต่ำ 1

            lblWave.Text = "Wave: " + currentWave;
        }

        private void SpawnBoss()
        {
            // ถ้ามีบอสเดิมอยู่ ให้ลบก่อน
            if (isBossAlive)
            {
                // ปลอดภัย: ลบบอสก่อน spawn ใหม่
                foreach (var e in enemies.ToArray())
                {
                    if (e.isBoss) RemoveEnemySafe(e);
                }
            }

            PictureBox bossPic = new PictureBox();
            bossPic.Size = new Size(150, 150);
            bossPic.SizeMode = PictureBoxSizeMode.StretchImage;
            bossPic.Image = Image.FromFile("boss.png");
            bossPic.BackColor = Color.Transparent;
            bossPic.BringToFront();
            bossPic.Location = new Point(rnd.Next(100, Math.Max(101, this.ClientSize.Width - 200)), 50); // ย้ายลงมาแค่ 50px

            int bossHP = 200;  // บอสเลือดเยอะ

            Enemy boss = new Enemy(bossPic, bossHP, true);
            enemies.Add(boss);
            this.Controls.Add(bossPic);

            // สร้างหรือตั้งค่าแถบเลือดบอส (ถ้ามีอยู่แล้วให้รีใช้)
            if (bossHPBar == null)
                bossHPBar = new ProgressBar();

            bossHPBar.Size = new Size(200, 20);
            bossHPBar.Maximum = bossHP;
            bossHPBar.Value = bossHP;
            bossHPBar.Location = new Point((this.ClientSize.Width - bossHPBar.Width) / 2, 10);
            bossHPBar.ForeColor = Color.Red;
            bossHPBar.BackColor = Color.Gray;
            bossHPBar.Visible = true;

            // ถ้ายังไม่มีใน Controls ให้ add
            if (!this.Controls.Contains(bossHPBar))
                this.Controls.Add(bossHPBar);

            isBossAlive = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelStartMenu.Visible = false;
            gameMoney = 0;
            lblMoney.Text = "Money: 0";

            ResetGame();
            gameTimer.Start();
            currentWave = 1;
            StartWave();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
            btnstart.Visible = true;
        }

        private void player_Click(object sender, EventArgs e)
        {
            player.BackColor = Color.Transparent;
            player.BringToFront();
        }

        private void lblScore_Click(object sender, EventArgs e) { }

        private void panelStartMenu_Paint(object sender, PaintEventArgs e) { }

        private void panelGame_Paint(object sender, PaintEventArgs e) { }

        private void btnShop_Click(object sender, EventArgs e)
        {
            panelShop.Visible = true;
            panelShop.BringToFront();
            lblShopMoney.Text = "Money: " + money;
        }

        private void btnEXIT_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelShop.Visible = false;
            lblMoneyMenu.Text = "Money: " + money;
            lblShopMoney.Text = "Money: " + money;
        }

        private void btnBuyHPBoost_Click_1(object sender, EventArgs e)
        {
            int price = 10;
            int healAmount = 20; // จำนวน HP ที่ฟื้น

            if (money < price)
            {
                MessageBox.Show("เงินไม่พอ!");
                return;
            }

            money -= price;
            hpBonus += healAmount;      // เพิ่มโบนัสเลือดสูงสุด
            playerHP += healAmount;     // ฟื้นเลือดผู้เล่นทันที

            if (playerHP > 100 + hpBonus)
                playerHP = 100 + hpBonus;

            hpBar.Maximum = 100 + hpBonus;
            hpBar.Value = playerHP;

            SaveMoney();
            lblShopMoney.Text = "Money: " + money;
            lblMoneyMenu.Text = "Money: " + money;

            MessageBox.Show("ซื้อ HP +" + healAmount + " แล้ว!");
        }

        private void btnBuyDamageUp_Click(object sender, EventArgs e)
        {
            int price = 100;

            if (money < price)
            {
                MessageBox.Show("เงินไม่พอ!");
                return;
            }

            money -= price;
            damageBonus += 10;

            SaveMoney();
            lblShopMoney.Text = "Money: " + money;
            lblMoneyMenu.Text = "Money: " + money;

            MessageBox.Show("ซื้อพลังโจมตี +10 แล้ว!");
        }

        private void btnGO_Restart_Click(object sender, EventArgs e)
        {
            panelGameOver.Visible = false;
            currentWave = 1;
            ResetGame();       // รีเซตทุกอย่าง
            gameTimer.Start(); // เริ่มใหม่
            StartWave();
            hpBar.Visible = true;
            lblScore.Visible = true;
            lblMoney.Visible = true;
            bulletLevel = 1;    // รีเซตกระสุน
        }

        private void btnGO_Exit_Click(object sender, EventArgs e)
        {
            panelGameOver.Visible = false;

            panelStartMenu.Visible = true;
            panelStartMenu.BringToFront();
            hpBar.Visible = true;
            lblScore.Visible = true;
            lblMoney.Visible = true;

            ResetGame();
        }
    }
}
