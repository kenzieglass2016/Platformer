using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Assignment05
{
	public partial class Engine : Form
	{
        public static Doctor doctor = new Doctor(100, 100);
		public static Sprite canvas = new Sprite();
		public static int enemyCount = 0;
		public static Enemy silent = new Enemy(1000, 200);
		public static Rectangle rect = new Rectangle(0, 0, 1400, 900, 200);
		public static bool win = false;
		public static bool lose = false;
		public static TextSprite text = new TextSprite(0, 0, "You Win!\nPress r to restart.");
		public static TextSprite loss = new TextSprite(0, 0, "You Lose.\nPress r to restart.");

		public Sprite Canvas
		{
			set { canvas = value; }
			get { return canvas; }
		}

		//public static Sprite parent = new Sprite();
		public static Engine form;
		public Thread rthread;
		public Thread uthread;
		public static int fps = 30;
		public static double running_fps = 30.0;
		public static bool rendering = false;
		public static bool updating = false;
		public static bool resetting = false;
		//public static SoundPlayer theme = new SoundPlayer(Properties.Resources.dwt);

		public Engine()
		{
			DoubleBuffered = true;
			InitializeComponent();
			//canvas = new Sprite();
			form = this;
			Size = new Size(1300, 800);
			StartPosition = FormStartPosition.CenterScreen;
			rthread = new Thread(new ThreadStart(render));
			uthread = new Thread(new ThreadStart(update));
			rthread.Start();
			uthread.Start();
			canvas.add(rect);
			canvas.add(text);
			canvas.add(loss);
		}

		public static void reset()
		{
			canvas.RemoveAll();
			resetting = true;
			//while (rendering || updating);
			doctor = new Doctor(100, 100);
            doctor.alive = true;
            canvas.csAdd(doctor);
			for (int i = 0; i < 13; i++)
			{
				Box box = new Box(i * 100, 0);
				canvas.csAdd(box);
				box = new Box(i * 100, 600);
				canvas.csAdd(box);
			}
			for (int i = 0; i < 7; i++)
			{
				Box box = new Box(0, i * 100);
				canvas.csAdd(box);
				box = new Box(1200, i * 100);
				canvas.csAdd(box);
			}
            silent = new Enemy(1000, 200);
			enemyCount = 1;
            canvas.csAdd(silent);
			canvas.add(rect);
			canvas.add(text);
			canvas.add(loss);
			resetting = false;
		}

		public static void render()
		{
			DateTime last = DateTime.Now;
			DateTime now = last;
			TimeSpan frameTime = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
				now = temp;
				TimeSpan diff = now - last;
				if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
					Thread.Sleep((frameTime - diff).Milliseconds);
				last = DateTime.Now;
				//form.Refresh();
				if (resetting) continue;
				if (enemyCount <= 0 && !lose)
				{
					rect.setColor(Rectangle.initColor);
					rect.setVisibility(true);
					text.setVisibility(true);
					text.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
					win = true;
				}
                else if (!doctor.alive && !win)
				{
					rect.setColor(Color.FromArgb(200, Color.Red));
					rect.setVisibility(true);
					loss.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
					loss.setVisibility(true);
					lose = true;
				}
				else
				{
					rect.setVisibility(false);
					text.setVisibility(false);
					loss.setVisibility(false);
					win = false;
					lose = false;
				}
				rendering = true;
				form.Invoke(new MethodInvoker(form.Refresh));
				rendering = false;
			}
		}

		public static void update()
		{
			DateTime last = DateTime.Now;
			DateTime now = last;
			TimeSpan frameTime = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				now = temp;
				TimeSpan diff = now - last;
				if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
					Thread.Sleep((frameTime - diff).Milliseconds);
				last = DateTime.Now;
				if (resetting) continue;
				updating = true;
				canvas.update();
				updating = false;
				if (!updating && !rendering)
				{
					canvas.queueClear();
					canvas.updateAllTracking();
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			//Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			/*base.OnPaint(e);
            e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
            parent.render(e.Graphics);
            e.Graphics.DrawString("Collisions: " + elephant.getCollisions().Count, new Font("Comic Sans MS", 10), Brushes.LawnGreen, 0, 0);
            e.Graphics.DrawString("Count: " + elephant.TrackedSprites.Count, new Font("Comic Sans MS", 10), Brushes.LawnGreen, 0, 20);
            e.Graphics.DrawString("Enemies Remaining: " + enemyCount, new Font("Comic Sans MS", 10), Brushes.LawnGreen, 0, 0);*/
			canvas.render(e.Graphics);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Down)
			{
                doctor.Vy = 5;
                doctor.Ay = -0.1f;
			}
			else if (e.KeyCode == Keys.Up)
			{
                doctor.Vy = -5;
                doctor.Ay = 0.1f;
			}
			else if (e.KeyCode == Keys.Left)
			{
                doctor.Vx = -5;
				doctor.Ax = 0.1f;
			}
			else if (e.KeyCode == Keys.Right)
			{
                doctor.Vx = 5;
                doctor.Ax = -0.1f;
			}
			/*else if (e.KeyCode == Keys.K)
            {
                jason.Kill();
            }*/
			else if (e.KeyCode == Keys.W)
			{
                doctor.shoot(0);
			}
			else if (e.KeyCode == Keys.D)
			{
                doctor.shoot(1);
			}
			else if (e.KeyCode == Keys.S)
			{
                doctor.shoot(2);
			}
			else if (e.KeyCode == Keys.A)
			{
                doctor.shoot(3);
			}
			else if (e.KeyCode == Keys.R)
			{
				reset();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			uthread.Abort();
			rthread.Abort();
		}

	}

}
