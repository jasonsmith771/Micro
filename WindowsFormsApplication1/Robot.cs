using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowsFormsApplication1
{
    public class Point3D
    {
        public double x;
        public double y;
        public double z;
    }
    public class tuple
    {
        public double t1;
        public double t2;
        public double t3;
        public double t4;
        public double t5;
        public double t6;
    }
    public class Robot
    {
        private readonly double pi = 3.14159265359;
        private readonly double dToR = 1.74532925199432E-02;

        //private:double atan2(  double y, double x);

        private double h; //dimension of the robot a is x direction, d  z-direction
        private double a1;
        private double a2;
        private double a3;
        private double a5;
        private double a6;
        private double d1;
        private double d4;
        private double d6;
        private double _t1; //dimension of the robot a is x direction, d  z-direction
        private double _t2;
        private double _t3;
        private double _t4;
        private double _t5;
        private double _t6;
        public WindowsFormsApplication1.MyForm f;
        //Given the angles of the motor, draw the robot consequently
        public tuple MoveToMotorAngles(double t1, double t2, double t3, double t4, double t5, double t6)
        {
            _t1 = t1;
            _t2 = t2;
            _t3 = t3;
            _t4 = t4;
            _t5 = t5;
            _t6 = t6;
            draw(t1, t2, t3, t4, t5, t6);
            return GetPosition();

        }
        //Given position and orientation, calculate the angles of the motors and draw robot consequently
        public tuple MoveToPositions(double x, double y, double z, double a, double b, double c)
        {
            tuple r = inverse(x, y, z, a, b, c);
            draw(r.t1, r.t2, r.t3, r.t4, r.t5, r.t6);
            _t1 = r.t1;
            _t2 = r.t2;
            _t3 = r.t3;
            _t4 = r.t4;
            _t5 = r.t5;
            _t6 = r.t6;
            return r;

        }
        //Return the actual position and orientation of the top of the arm6
        public tuple GetPosition()
        {
            double nx;
            double ny;
            double nz;
            double sx;
            double sy;
            double sz;
            double ax;
            double ay;
            double az;
            double par = 30 * pi / 180;
            double s1;
            double c1;
            double s2;
            double c2;
            double s3;
            double c3;
            double s23;
            double c23;
            double s4;
            double c4;
            double s5;
            double c5;
            double s6;
            double c6;
            s1 = Math.Sin(_t1);
            s2 = Math.Sin(_t2);
            s3 = Math.Sin(_t3);
            s23 = Math.Sin(_t2 + _t3);
            s4 = Math.Sin(_t4);
            s5 = Math.Sin(_t5);
            s6 = Math.Sin(_t6);
            c1 = Math.Cos(_t1);
            c2 = Math.Cos(_t2);
            c3 = Math.Cos(_t3);
            c23 = Math.Cos(_t2 + _t3);
            c4 = Math.Cos(_t4);
            c5 = Math.Cos(_t5);
            c6 = Math.Cos(_t6);
            //nx = ((s1*s4 - c1*s23*c4)*c5 - c1*c23*s5)*s6 + (-c1*s23*s4 - s1*c4)*c6;
            //ny = ((-c1*s4 - s1*s23*c4)*c5 - s1*c23*s5)*s6 + (c1*c4 - s1*s23*s4)*c6;
            nz = (c23 * c4 * c5 - s23 * s5) * s6 + c23 * s4 * c6;
            //sx = ((s1*s4 - c1*s23*c4)*c5 - c1*c23*s5)*c6 - (-c1*s23*s4 - s1*c4)*s6;
            //sy = ((-c1*s4 - s1*s23*c4)*c5 - s1*c23*s5)*c6 - (c1*c4 - s1*s23*s4)*s6;
            sz = (c23 * c4 * c5 - s23 * s5) * c6 - c23 * s4 * s6;
            ax = (s1 * s4 - c1 * s23 * c4) * s5 + c1 * c23 * c5;
            ay = (-c1 * s4 - s1 * s23 * c4) * s5 + s1 * c23 * c5;
            az = c23 * c4 * s5 + s23 * c5;


            //nx = round2(nx, 6);
            //ny = round2(ny, 6);
            nz = round2(nz, 6);
            //sy = round2(sy, 6);
            sz = round2(sz, 6);
            az = round2(az, 6);
            ay = round2(ay, 6);
            ax = round2(ax, 6);
            Point3D p = Arm6(_t1, _t2, _t3, _t4, _t5, _t6, 0, 0, d6);
            tuple t = new tuple();


            t.t1 = p.x;
            t.t2 = p.y;
            t.t3 = p.z;
            //I calculate this value with brute force, see below
            t.t5 = Math.Atan2(-az, Math.Sqrt(ax * ax + ay * ay)); // it works also with atan2(-az, sqrt(sz*sz + nz*nz));
            t.t4 = Math.Atan2(ay / Math.Cos(t.t5), ax / Math.Cos(t.t5));
            t.t6 = Math.Atan2(sz / Math.Cos(t.t5), nz / Math.Cos(t.t5)) - pi / 2;


            //This quoted procedure needs to find, brute force, the right configuration of parameters... there is to go crazy
            //Remember to initiate the class MyForm in Robot::Robot

            //double m[9];
            //System::Text::StringBuilder^ st = gcnew System::Text::StringBuilder("");
            //m[0] = round2(nx,6);
            //m[1] = round2(ny, 6);
            //m[2] = round2(nz, 6);
            //m[3] = round2(sx, 6);
            //m[4] = round2(sy, 6);
            //m[5] = round2(sz, 6);
            //m[6] = round2(ax, 6);
            //m[7] = round2(ay, 6);
            //m[8] = round2(az, 6);

            //for (int i = 0; i < 9;i++)
            //{
            //	for (int j = 0; j < 9; j++)
            //	{

            //		/*for (int k = 0; k < 9; k++)
            //		{
            //		*/	st->Append(i);
            //			st->Append(j);
            //		// st->Append(k);
            //			st->Append("=");
            //			st->Append(atan2(m[i] / cos(t->t5), m[j] / cos(t->t5)) / dToR);
            //			// st->Append(atan2(m[i],sqrt(m[j]*m[j]+m[k]*m[k]))/dToR);
            //			st->Append("\r\n");
            //	/*}*/

            //	}


            //}


            //f->textBox1->Text = st->ToString();
           return t;
        }
        public System.Windows.Forms.PictureBox PicBox;
        public System.Drawing.Bitmap part;
        //personal function to round at given n places
        private double round2(double x, int n)
        {
            double v;
            v = Math.Pow((double)10, n);
            return Math.Round(x * v) / v;
        }
        //Given position and orientation, calculate the angles of the motors
        private tuple inverse(double x, double y, double z, double a, double b, double c)
        {
            tuple p;
            p = new tuple();
            double pWx;
            double pWy;
            double pWz;
            double px;
            double py;
            double pz;
            double s3;
            double c3;
            double s2;
            double c2;
            double xi;
            double fi;

            int sgn;
            pWx = x - Math.Cos(a) * Math.Cos(b) * (d6 + a5);
            pWy = y - Math.Sin(a) * Math.Cos(b) * (d6 + a5);
            pWz = z + Math.Sin(b) * (d6 + a5);
            double u;
            int sg = 1;
            p.t1 = Math.Atan2(pWy, pWx);
            ///////Dunno what it does
            //if ((int)(pWx * pWx + pWy * pWy - a1 * a1 < 0) != 0)
            //{
            //    sg = -1;
            //}
            //distance of the wirst from the Motor 2
            px = pWx - a1 * Math.Cos(p.t1);
            py = pWy - a1 * Math.Sin(p.t1);
            pz = pWz - d1 - h;




            c3 = (px * px + py * py + pz * pz - Math.Pow(a3 + d4, 2) - a2 * a2) / (2 * (a3 + d4) * a2);
            s3 = -Math.Sqrt(1 - c3 * c3);

            p.t3 = Math.Atan2(s3, c3);

            //the follow code show in the MyForm f, some parameters. MyForm get shown in the creator
            //f->textBox1->Text = System::String::Concat("c3num =", (px*px + py*py + pz*pz - pow((a3 + d4), 2) ), "\r\n"
            //	, "mod =", px*px + py*py + pz*pz, "\r\n"
            //	, "pWx =", pWx, "\r\n"
            //	, "pWy =", pWy, "\r\n"
            //	, "pWz =", pWz, "\r\n"
            //	, "px =", px, "\r\n"
            //	, "py =", py, "\r\n"
            //	, "pz =", pz, "\r\n"
            //	, "c3 =", c3, "\r\n"
            //	, "s3 =", s3, "\r\n"
            //	, "t3 =", p->t3, "\r\n"
            //	);
            // http://www.rob.uni-luebeck.de/Lehre/2008w/Robotik/Vorlesung/Robotik1VL5_1_vers1.pdf  pag. 50
            xi = Math.Atan2(pz, sg * Math.Sqrt(px * px + py * py)); // sg is my addition
            fi = Math.Atan2((a3 + d4) * s3, (a2 + (a3 + d4) * c3));

            p.t2 = xi - fi;

            double nx;
            double ny;
            double nz;
            double sx;
            double ax;
            double sa;
            double sb;
            double sc;
            double s1;
            double ca;
            double cb;
            double cc;
            double c1;
            double c23;
            double s23;
            sa = Math.Sin(a);
            sb = Math.Sin(b);
            sc = Math.Sin(c);
            s1 = Math.Sin(p.t1);
            s23 = Math.Sin(p.t3 + p.t2);

            ca = Math.Cos(a);
            cb = Math.Cos(b);
            cc = Math.Cos(c);
            c1 = Math.Cos(p.t1);
            c23 = Math.Cos(p.t3 + p.t2);

            nx = round2(-sb * s23 + cb * c23 * Math.Cos(a - p.t1), 6);
            ny = round2(-cb * s23 * Math.Cos(a - p.t1) - sb * c23, 6);
            nz = round2(cb * Math.Sin(p.t1 - a), 6);
            sx = round2(cb * sc * s23 + (sa * sb * sc + ca * cc) * s1 * c23 + (ca * sb * sc - sa * cc) * c1 * c23, 6);
            ax = round2(cb * cc * s23 + (sa * sb * cc - ca * sc) * s1 * c23 + (sa * sc + ca * sb * cc) * c1 * c23, 6);

            p.t5 = Math.Atan2(Math.Sqrt(nz * nz + ny * ny), (nx));
            if (p.t5 < 0)
            {
                p.t4 = Math.Atan2((-nz), (-ny));

                p.t5 = Math.Atan2(-Math.Sqrt(nz * nz + ny * ny), (nx));

            }
            else
            {
                //p->t4 = atan2((nz), (ny));

                //p->t5 = atan2(sqrt(nz *nz + ny *ny), (nx));
                p.t4 = Math.Atan2((nz), (ny));

            }
            p.t6 = Math.Atan2(-sx, -ax);
            if (sg < 0)
            {
                p.t6 = Math.Atan2(sx, ax);

                if (p.t6 < 0)
                {
                    p.t6 = Math.Atan2(-sx, -ax);
                }
                else
                {
                    p.t6 = Math.Atan2(sx, ax);
                }
            }


            return p;

        }
        //transforms a 3d Point in a 2D with assonemtrie
        private Point pToChart(Point3D p)
        {
            return pToChart(p.x, p.y, p.z);

        }
        //transforms a 3d Point in a 2D with assonemtrie
        private Point pToChart(double _x, double _y, double _z)
        {

            Point pr = new Point();
            double x_x = x / 2 + _y - _x * 0.35355339059;
           double y_y = y / 2 - _z + _x * 0.35355339059;
            pr.X = (int)x_x;    // x / 2 + _y - _x * 0.35355339059;
            pr.Y = (int)y_y; // y / 2 - _z + _x * 0.35355339059;
            return pr;

        }
        //The equation of the First arm.  Convert the _x,_y,_z, in the coordinate system of the arm. 
        private Point3D Arm1(double t1, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            p.x = Math.Cos(t1) * _x - Math.Sin(t1) * _y;
            p.y = Math.Cos(t1) * _y + Math.Sin(t1) * _x;
            p.z = h + _z; // +d1;
            return p;

        }
        //If contains the graphical array of points or equation of the arm
        private void DrawArm1(Graphics g, Pen rPen, double t1)
        {
            double[,] parm =
            {
       {0, 0, 0},
       {0, 0, d1},
       {a1, 0, d1},
       {a1, 0, 0},
       {0, 0, 0}
   };

            for (int i = 0; i < 4; i++)
            {

                g.DrawLine(rPen, pToChart(Arm1(t1, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm1(t1, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }

        }
        //see arm1
        private Point3D Arm2(double t1, double t2, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            double s1;
            double c1;
            double s2;
            double c2;
            s1 = Math.Sin(t1);
            s2 = Math.Sin(t2);
            c1 = Math.Cos(t1);
            c2 = Math.Cos(t2);

            p.x = s1 * _z - c1 * s2 * _y + c1 * c2 * _x + a1 * c1;
            p.y = -c1 * _z - s1 * s2 * _y + s1 * c2 * _x + a1 * s1;
            p.z = h + c2 * _y + s2 * _x + d1;
            return p;

        }
        //see arm1
        private void DrawArm2(Graphics g, Pen rPen, double t1, double t2)
        {
            double[,] parm =
            {
        {0, 0, -10},
        {a2, 0, -10},
        {a2, 0, 10},
        {0, 0, 10},
        {0, 0, -10}
    };
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(rPen, pToChart(Arm2(t1, t2, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm2(t1, t2, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }

        }
        //see arm1
        private Point3D Arm3(double t1, double t2, double t3, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            double s1;
            double c1;
            double s2;
            double c2;
            double s3;
            double c3;
            double s23;
            double c23;
            double s4;
            double c4;
            s1 = Math.Sin(t1);
            s2 = Math.Sin(t2);
            s23 = Math.Sin(t2 + t3);
            c1 = Math.Cos(t1);
            c2 = Math.Cos(t2);
            c23 = Math.Cos(t2 + t3);

            p.x = s1 * _z - c1 * s23 * _y + c1 * c23 * _x + a2 * c1 * c2 + a1 * c1;
            p.y = -c1 * _z - s1 * s23 * _y + s1 * c23 * _x + a2 * s1 * c2 + a1 * s1;
            p.z = h + c23 * _y + s23 * _x + a2 * s2 + d1;
            return p;

        }
        //see arm1
        private void DrawArm3(Graphics g, Pen rPen, double t1, double t2, double t3)
        {
            double[,] parm =
            {
        {0, 0, -10},
        {a3, 0, -10},
        {a3, 0, 10},
        {0, 0, 10},
        {0, 0, -10}
    };
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(rPen, pToChart(Arm3(t1, t2, t3, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm3(t1, t2, t3, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }
        }
        //see arm1
        private Point3D Arm4(double t1, double t2, double t3, double t4, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            double s1;
            double c1;
            double s2;
            double c2;
            double s3;
            double c3;
            double s23;
            double c23;
            double s4;
            double c4;
            s1 = Math.Sin(t1);
            s2 = Math.Sin(t2);

            s23 = Math.Sin(t2 + t3);
            s4 = Math.Sin(t4);
            c1 = Math.Cos(t1);
            c2 = Math.Cos(t2);

            c23 = Math.Cos(t2 + t3);
            c4 = Math.Cos(t4);

            p.x = c1 * c23 * _z + (s1 * s4 - c1 * s23 * c4) * _y + (-c1 * s23 * s4 - s1 * c4) * _x + a3 * c1 * c23 + a2 * c1 * c2 + a1 * c1;
            p.y = s1 * c23 * _z + (-c1 * s4 - s1 * s23 * c4) * _y + (c1 * c4 - s1 * s23 * s4) * _x + a3 * s1 * c23 + a2 * s1 * c2 + a1 * s1;
            p.z = h + s23 * _z + c23 * c4 * _y + c23 * s4 * _x + a3 * s23 + a2 * s2 + d1;

            return p;

        }
        //see arm1
        private void DrawArm4(Graphics g, Pen rPen, double t1, double t2, double t3, double t4)
        {
            double[,] parm =
            {
        {-10, 0, 0},
        {-10, 0, d4},
        {10, 0, d4},
        {10, 0, 0},
        {-10, 0, 0}
    };
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(rPen, pToChart(Arm4(t1, t2, t3, t4, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm4(t1, t2, t3, t4, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }

        }
        //see arm1
        private Point3D Arm5(double t1, double t2, double t3, double t4, double t5, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            double s1;
            double c1;
            double s2;
            double c2;
            double s3;
            double c3;
            double s23;
            double c23;
            double s4;
            double c4;
            double s5;
            double c5;
            s1 = Math.Sin(t1);
            s2 = Math.Sin(t2);
            s3 = Math.Sin(t3);
            s23 = Math.Sin(t2 + t3);
            s4 = Math.Sin(t4);
            s5 = Math.Sin(t5);
            c1 = Math.Cos(t1);
            c2 = Math.Cos(t2);
            c3 = Math.Cos(t3);
            c23 = Math.Cos(t2 + t3);
            c4 = Math.Cos(t4);
            c5 = Math.Cos(t5);

            p.x = (c1 * s23 * s4 + s1 * c4) * _z + ((s1 * s4 - c1 * s23 * c4) * c5 - c1 * c23 * s5) * _y + ((s1 * s4 - c1 * s23 * c4) * s5 + c1 * c23 * c5) * _x + d4 * c1 * c23 + a3 * c1 * c23 + a2 * c1 * c2 + a1 * c1;

            p.y = (s1 * s23 * s4 - c1 * c4) * _z + ((-c1 * s4 - s1 * s23 * c4) * c5 - s1 * c23 * s5) * _y + ((-c1 * s4 - s1 * s23 * c4) * s5 + s1 * c23 * c5) * _x + d4 * s1 * c23 + a3 * s1 * c23 + a2 * s1 * c2 + a1 * s1;

            p.z = h - c23 * s4 * _z + (c23 * c4 * c5 - s23 * s5) * _y + (c23 * c4 * s5 + s23 * c5) * _x + d4 * s23 + a3 * s23 + a2 * s2 + d1;

            return p;

        }
        //see arm1
        private void DrawArm5(Graphics g, Pen rPen, double t1, double t2, double t3, double t4, double t5)
        {
            double[,] parm =
            {
        {0, 0, -10},
        {a5, 0, -10},
        {a5, 0, 10},
        {0, 0, 10},
        {0, 0, -10}
    };
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(rPen, pToChart(Arm5(t1, t2, t3, t4, t5, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm5(t1, t2, t3, t4, t5, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }


        }
        //see arm1
        private Point3D Arm6(double t1, double t2, double t3, double t4, double t5, double t6, double _x, double _y, double _z)
        {
            Point3D p;
            p = new Point3D();
            double s1;
            double c1;
            double s2;
            double c2;
            double s3;
            double c3;
            double s23;
            double c23;
            double s4;
            double c4;
            double s5;
            double c5;
            double s6;
            double c6;
            s1 = Math.Sin(t1);
            s2 = Math.Sin(t2);
            s3 = Math.Sin(t3);
            s23 = Math.Sin(t2 + t3);
            s4 = Math.Sin(t4);
            s5 = Math.Sin(t5);
            s6 = Math.Sin(t6);
            c1 = Math.Cos(t1);
            c2 = Math.Cos(t2);
            c3 = Math.Cos(t3);
            c23 = Math.Cos(t2 + t3);
            c4 = Math.Cos(t4);
            c5 = Math.Cos(t5);
            c6 = Math.Cos(t6);

            p.x = ((s1 * s4 - c1 * s23 * c4) * s5 + c1 * c23 * c5) * _z + (((s1 * s4 - c1 * s23 * c4) * c5 - c1 * c23 * s5) * c6 - (-c1 * s23 * s4 - s1 * c4) * s6) * _y + (((s1 * s4 - c1 * s23 * c4) * c5 - c1 * c23 * s5) * s6 + (-c1 * s23 * s4 - s1 * c4) * c6) * _x + a5 * ((s1 * s4 - c1 * s23 * c4) * s5 + c1 * c23 * c5) + d4 * c1 * c23 + a3 * c1 * c23 + a2 * c1 * c2 + a1 * c1;

            p.y = ((-c1 * s4 - s1 * s23 * c4) * s5 + s1 * c23 * c5) * _z + (((-c1 * s4 - s1 * s23 * c4) * c5 - s1 * c23 * s5) * c6 - (c1 * c4 - s1 * s23 * s4) * s6) * _y + (((-c1 * s4 - s1 * s23 * c4) * c5 - s1 * c23 * s5) * s6 + (c1 * c4 - s1 * s23 * s4) * c6) * _x + a5 * ((-c1 * s4 - s1 * s23 * c4) * s5 + s1 * c23 * c5) + d4 * s1 * c23 + a3 * s1 * c23 + a2 * s1 * c2 + a1 * s1;

            p.z = h + (c23 * c4 * s5 + s23 * c5) * _z + ((c23 * c4 * c5 - s23 * s5) * c6 - c23 * s4 * s6) * _y + ((c23 * c4 * c5 - s23 * s5) * s6 + c23 * s4 * c6) * _x + a5 * (c23 * c4 * s5 + s23 * c5) + d4 * s23 + a3 * s23 + a2 * s2 + d1;

            return p;

        }
        //see arm1
        private void DrawArm6(Graphics g, Pen rPen, double t1, double t2, double t3, double t4, double t5, double t6)
        {
            double[,] parm =
            {
        {-20, 0, 0},
        {-20, 0, d6},
        {20, 0, d6},
        {20, 0, 0},
        {-20, 0, 0}
    };
            for (int i = 0; i < 4; i++)
            {
                g.DrawLine(rPen, pToChart(Arm6(t1, t2, t3, t4, t5, t6, parm[i, 0], parm[i, 1], parm[i, 2])), pToChart(Arm6(t1, t2, t3, t4, t5, t6, parm[i + 1, 0], parm[i + 1, 1], parm[i + 1, 2])));
            }

        }


        public double x;
        public double y;


        //Creator: in input size of the image
        public Robot(double _x, double _y)
        {
            //'the dimension of th robot
            h = 20; // 'base to First Joint
            a1 = 60; // 'First Joint to second Joint x-move
            d1 = 45; //'First Joint to second Joint z-move

            a2 = 120; // 'second Joint to third Joint x-move
            a3 = 20;
            d4 = 100;
            a5 = 20;
            a6 = 10;
            d6 = 30;



            x = _x;
            y = _y;
            draw(_t1, _t2, _t3, _t4, _t5, _t6);

            //f = gcnew RobotSimulator::MyForm;
            //	f->Show();


        }
        //It draws the Robot at the given angles.
        public void draw(double t1, double t2, double t3, double t4, double t5, double t6)
        {

            part = new System.Drawing.Bitmap((int)x, (int)y);
            System.Drawing.Graphics g;
            g = System.Drawing.Graphics.FromImage(part);
            g.FillRectangle(System.Drawing.Brushes.Black, 0, 0, (int)x, (int)y);
            Pen AxPen;
            AxPen = new Pen(Color.Blue, 3);
            Pen rPen;
            rPen = new Pen(Color.Red, 3);
            //Point 3d is my struct for contain a point in the space
            Point3D pi;
            Point3D pf;
            pi = new Point3D();
            pf = new Point3D();
            //these are the main axes
            pi.z = 0;
            pf.z = 0;
            pi.x = 0;
            pi.y = 0;
            pf.x = x;
            pf.y = 0;

            g.DrawLine(AxPen, pToChart(pi), pToChart(pf));

            pi.x = 0;
            pi.y = 0;
            pf.x = 0;
            pf.y = y;
            g.DrawLine(AxPen, pToChart(pi), pToChart(pf));
            pi.x = 0;
            pi.y = 0;
            pf.x = 0;
            pf.y = 0;
            pf.z = y;

            g.DrawLine(AxPen, pToChart(pi), pToChart(pf));


            //Draws the base
            for (int i = 0; i < 360; i++)
            {
                g.DrawLine(rPen, pToChart(20 * Math.Cos(i * dToR), 20 * Math.Sin(i * dToR), 0), pToChart(20 * Math.Cos((i + 1) * dToR), 20 * Math.Sin((i + 1) * dToR), 0));
                g.DrawLine(rPen, pToChart(20 * Math.Cos(i * dToR), 20 * Math.Sin(i * dToR), h), pToChart(20 * Math.Cos((i + 1) * dToR), 20 * Math.Sin((i + 1) * dToR), h));
            }

            g.DrawLine(rPen, pToChart(0, 20, 0), pToChart(0, 20, h));
            g.DrawLine(rPen, pToChart(0, -20, h), pToChart(0, -20, 0));

            //Draw the armes
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            //ORIGINAL LINE: DrawArm1(g, rPen, t1);
            //  DrawArm1(new System.Drawing.Graphics(g), new Pen(rPen), t1);

            DrawArm1(g, rPen, t1);
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            DrawArm2(g, rPen, t1, t2);
            //DrawArm2(new System.Drawing.Graphics(g), new Pen(rPen), t1, t2);
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            DrawArm3(g, rPen, t1, t2, t3);
            //DrawArm3(new System.Drawing.Graphics(g), new Pen(rPen), t1, t2, t3);
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            DrawArm4(g, rPen, t1, t2, t3,t4);
            //DrawArm4(new System.Drawing.Graphics(g), new Pen(rPen), t1, t2, t3, t4);
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            DrawArm5(g, rPen, t1, t2, t3, t4,t5);
           // DrawArm5(new System.Drawing.Graphics(g), new Pen(rPen), t1, t2, t3, t4, t5);
            //C++ TO C# CONVERTER WARNING: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
            DrawArm6(g, rPen, t1, t2, t3, t4, t5,t6);
            //DrawArm6(new System.Drawing.Graphics(g), new Pen(rPen), t1, t2, t3, t4, t5, t6);
        }
        //Return the image
        public Bitmap GetImage()
        {
            return part;
        }
        //Resize the image
        public void Resize(double _x, double _y)
        {
            x = _x;
            y = _y;
            draw(_t1, _t2, _t3, _t4, _t5, _t6);
        }

        //	void getShape(double * b, double a, double d);
    }
    //double Robot::atan2(double y, double  x)
    //{
    //
    //	if (x > 0)
    //		return atan(y / x);
    //	else
    //	{
    //		if (x < 0)
    //		{
    //			if (y >= 0)
    //				return atan(y / x) + pi;
    //			else
    //				return atan(y / x) - pi;
    //		}
    //		else
    //		{
    //			if (y>0) return pi / 2;
    //			if (y<0) return -pi / 2;
    //			return 0;
    //				
    //		}
    //	}	
    //
    //
    //}
    //
}
