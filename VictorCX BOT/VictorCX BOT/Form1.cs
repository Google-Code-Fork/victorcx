using System;
using Tibia;
using Tibia.Objects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Client client;
        private Player player;
        private Map map;
        private Inventory inventory;
        private Screen screen;
        private BattleList blist;
        private Tibia.Objects.Console console;
        private int cFloor;
        private int cZ;
        private string sign;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = Tibia.Util.ClientChooser.ShowBox();
            if (client == null)
                {
            MessageBox.Show("No active client.");
            Application.Exit();
                }
            map = new Map(client);
            player = client.GetPlayer();
            screen = new Tibia.Objects.Screen(client);
            blist = new BattleList(client);
            player = client.GetPlayer();
            map = new Map(client);
            uxLongTimer.Start();
            console = new Tibia.Objects.Console(client);
            inventory = new Inventory(client);

            KeyboardHook.Enable();
            KeyboardHook.KeyDown = null;
            KeyboardHook.KeyDown += new KeyboardHook.KeyboardHookHandler(delegate(Keys key)
            {
                if (client.IsActive)
                {
                    if (key == Keys.Add)
                    {
                        map.ShowNames(true);
                        map.FullLight(true);
                        if (map.ShowFloor(cFloor + 1, true))
                            cFloor++;
                        if (cFloor == 0)
                        {
                            map.ShowFloor(0, false);
                            map.ShowNames(false);
                            map.FullLight(false);
                        }
                        if (cFloor > 0) sign = "+"; else sign = "";
                        client.Statusbar = "LevelSpy => Floor: " + sign + cFloor;
                    }
                    if (key == Keys.Subtract)
                    {
                        if (cFloor == 0 && player.Z == 7)
                        {
                            map.ShowFloor(0, true);
                            client.Statusbar = "LevelSpy => Removing roofs.";
                        }
                        else
                        {
                            map.ShowNames(true);
                            map.FullLight(true);
                            if (map.ShowFloor(cFloor - 1, true))
                                cFloor--;
                            if (cFloor == 0)
                            {
                                map.ShowFloor(0, false);
                                map.ShowNames(false);
                                map.FullLight(false);
                            }
                            if (cFloor > 0) sign = "+"; else sign = "";
                            client.Statusbar = "LevelSpy => Floor: " + sign + cFloor;
                        }
                    }
                    if (key == Keys.Subtract || key == Keys.Add)
                        return false;
                }
                return true;
            });
            KeyboardHook.KeyDown += null;

        }

        private void reemplazarArbolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map.ReplaceTrees();
        }

        private void goblinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blist.GetCreature("goblin").Attack();
        }

        private void enableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            player.Light = Tibia.Constants.LightSize.Full;
            player.LightColor = Tibia.Constants.LightColor.White;
        }

        private void disableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            player.Light = Tibia.Constants.LightSize.None;
            player.LightColor = Tibia.Constants.LightColor.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cZ != player.Z)
            {
                map.ShowFloor(0, false);
                map.ShowNames(false);
                map.FullLight(false);
            }
            cZ = player.Z;
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 0.1.1 Pre-alpha");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox3.Invoke(new EventHandler(delegate
                {
                    textBox3.Text = i.Profession;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox4.Invoke(new EventHandler(delegate
                {
                    textBox4.Text = i.Sex;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox5.Invoke(new EventHandler(delegate
                {
                    textBox5.Text = i.World;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox6.Invoke(new EventHandler(delegate
                {
                    textBox6.Text = i.AccountStatus;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox7.Invoke(new EventHandler(delegate
                {
                    textBox7.Text = i.GuildTitle;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox8.Invoke(new EventHandler(delegate
                {
                    textBox8.Text = i.GuildName;

                }));
            });
            Website.LookupPlayer((textBox1.Text), delegate(Website.CharInfo i)
            {
                textBox9.Invoke(new EventHandler(delegate
                {
                    textBox9.Text = i.Location;

                }));
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.SetOT("(textBox2.Text)", 7171);
            MessageBox.Show("Tibia IP Changed!");
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                {
                if (player.HP < Int32.Parse(textBox12.Text) && player.Mana > Int32.Parse(textBox13.Text))
                    {
                    console.Say(textBox11.Text);
                    }
                }
        }

        private void EatFood_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                {
            inventory.UseItem(Tibia.Constants.Items.Food.DragonHam);
            inventory.UseItem(Tibia.Constants.Items.Food.Ham);
            inventory.UseItem(Tibia.Constants.Items.Food.Apple);
            inventory.UseItem(Tibia.Constants.Items.Food.Banana);
            inventory.UseItem(Tibia.Constants.Items.Food.Blueberry);
            inventory.UseItem(Tibia.Constants.Items.Food.Bread);
            inventory.UseItem(Tibia.Constants.Items.Food.BrownBread);
            inventory.UseItem(Tibia.Constants.Items.Food.BrownMushroom);
            inventory.UseItem(Tibia.Constants.Items.Food.Carrot);
            inventory.UseItem(Tibia.Constants.Items.Food.Cheese);
            inventory.UseItem(Tibia.Constants.Items.Food.Cherry);
            inventory.UseItem(Tibia.Constants.Items.Food.Coconut);
            inventory.UseItem(Tibia.Constants.Items.Food.Cookie);
            inventory.UseItem(Tibia.Constants.Items.Food.Corncob);
            inventory.UseItem(Tibia.Constants.Items.Food.Egg);
            inventory.UseItem(Tibia.Constants.Items.Food.Fish);
            inventory.UseItem(Tibia.Constants.Items.Food.Grapes);
            inventory.UseItem(Tibia.Constants.Items.Food.GreenMushroom);
            inventory.UseItem(Tibia.Constants.Items.Food.Meat);
            inventory.UseItem(Tibia.Constants.Items.Food.Mellon);
            inventory.UseItem(Tibia.Constants.Items.Food.Orange);
            inventory.UseItem(Tibia.Constants.Items.Food.Roll);
            inventory.UseItem(Tibia.Constants.Items.Food.Salmon);
            inventory.UseItem(Tibia.Constants.Items.Food.WhiteMushroom);
                }
        }

        private void actualizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://victorcx.wordpress.com/actualizaciones/");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                EatFood.Enabled = true;
            else
                EatFood.Enabled = false;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                SpellHealer.Enabled = true;
            else
                SpellHealer.Enabled = false;
        }

        private void ReplaceTrees_Tick(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
                {
             map.ReplaceTrees();
                }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                ReplaceTrees.Enabled = true;
            else
                ReplaceTrees.Enabled = false;

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                AttackMonsters.Enabled = true;
            else
                AttackMonsters.Enabled = false;
        }

        private void autoAtacarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AttackMonsters_Tick(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                {
            blist.GetCreature("goblin").Attack();
            blist.GetCreature("goblin assassin").Attack();
                }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PlayerPorcents_Tick(object sender, EventArgs e)
        {

            screen.DrawCreatureText(player.Id, new Location(0, -10, 0), Color.Green,
            Tibia.Constants.ClientFont.NormalBorder, player.HPBar.ToString() + "%");
        }
}
}

