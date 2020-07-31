﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodoBiseccion
{
    public partial class Form1 : Form
    {
        public int i = 0;
        public double aI = -2;
        public double bI = 2;
        private List<iteraciones> listIte { get; set; }
        public Form1()
        {
            listIte = DoIter();
            InitializeComponent();
            this.CenterToScreen();
        }

        public double calcXr(double a, double b)
        {
            return (a + b) / 2;
        }

        public double calcFa(double a)
        {
            return Math.Sin(a)+2*a-1;
        }

        public double calcFxr(double xr)
        {
            return Math.Sin(xr) + 2 * xr - 1;
        }

        public double calcFaFxr(double fa, double fxr)
        {
            return fa*fxr;
        }

        public double calcError(double xrN, double xrA)
        {
            return Math.Abs((xrN - xrA) / xrN) * 100;
        }

        private List<iteraciones> DoIter()
        {
            var list = new List<iteraciones>();
            list.Add(new iteraciones(1, aI, bI, calcXr(aI, bI), calcFa(aI), calcFxr(calcXr(aI, bI)), calcFaFxr(calcFa(aI), calcFxr(calcXr(aI, bI)))));
            double error, a, b, xr, fa, fxr, faFxr, comp;
            int it = 1;
            do
            {
                iteraciones anterior = list.Last();
                it++;
                if (anterior.faFxr < 0)
                {
                    a = anterior.a;
                    b = anterior.xr;
                }
                else
                {
                    a = anterior.xr;
                    b = anterior.b;
                }

                xr = calcXr(a, b);
                fa = calcFa(a);
                fxr = calcFxr(xr);
                faFxr = calcFaFxr(fa, fxr);
                error = calcError(xr, anterior.xr);
                comp = Math.Truncate(error * 1000);
                list.Add(new iteraciones(it, a, b, xr, fa, fxr, faFxr, error));
            }
            while ((comp/1000) != 0.001 || (comp/1000) < 0.001);

            return list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ite = listIte;
            iteraciones iF = ite.Last();
            this.textBox1.Text = iF.xr.ToString();
            //this.tabla.AutoGenerateColumns = true;
            tabla.DataSource = ite;
        }
    }
}
