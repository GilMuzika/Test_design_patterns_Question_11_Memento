using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Test_design_patterns_Question_11_Memento.memento;
using Test_design_patterns_Question_11_Memento.UI;

namespace Test_design_patterns_Question_11_Memento
{
    public partial class Form1 : Form
    {
        private LabelField[,] _matrix = new LabelField[5, 5]; // only odd numbers in "_matrix" array initializer        
        private Panel _pnlMatrixHolder;
        private LabelField _mySoldier;
        private char _mySoldierCharacter = '1';
        private int _pnlMatrixHolderPadding = 10;
        private int _labelFieldLocationStep = 5;
        private int _labelFieldWidth = 25;
        private int _labelFieldHeight = 25;

        //for memento
        Originator _originator = new Originator();
        CareTaker _careTaker = new CareTaker();


        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private int _count = 0;
        private void Initialize()
        {
            _pnlMatrixHolder = new Panel();
            _pnlMatrixHolder.Location = new Point(_pnlMatrixHolderPadding, _pnlMatrixHolderPadding);
            _pnlMatrixHolder.Width = _pnlMatrixHolderPadding * 2 + _matrix.GetLength(0) * _labelFieldWidth + (_matrix.GetLength(0)-1) * _labelFieldLocationStep;
            _pnlMatrixHolder.Height = _pnlMatrixHolderPadding * 2 + _matrix.GetLength(1) * _labelFieldHeight + (_matrix.GetLength(1) - 1) * _labelFieldLocationStep;

            Label lblInfo = new Label();
            lblInfo.Location = new Point(_pnlMatrixHolderPadding, _pnlMatrixHolderPadding * 2 + _pnlMatrixHolder.Height);
            lblInfo.Text = "For saving, press s \nFor loading, press l ";
            lblInfo.AutoSize = true;
            this.Controls.Add(lblInfo);

            _pnlMatrixHolder.drawBorder(5, Color.Black);
            this.Controls.Add(_pnlMatrixHolder);
            this.ClientSize = new Size(_pnlMatrixHolder.Width + _pnlMatrixHolderPadding * 2, _pnlMatrixHolder.Height + _pnlMatrixHolderPadding * 2 + 100 );
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            CreateMatrix();

            this.KeyDown += (object sender, KeyEventArgs e) =>
            {
                                
                switch(e.KeyCode)
                {
                    case Keys.Up:
                        MoveSoldier(_mySoldier.UP);                        
                        break;
                    case Keys.Down:                        
                        MoveSoldier(_mySoldier.DowN);                        
                        break;
                    case Keys.Left:                        
                        MoveSoldier(_mySoldier.LefT);                        
                        break;
                    case Keys.Right:                        
                        MoveSoldier(_mySoldier.RighT);                        
                        break;
                }                
            };
            

            this.KeyPress += (object sendr, KeyPressEventArgs e) => 
            {
                //MessageBox.Show(e.KeyChar.ToString());

                if (e.KeyChar == '+' || e.KeyChar == '-')
                {
                    _mySoldier.Character = _mySoldierCharacter;
                    char c = ChangeSoldier(_mySoldier, e.KeyChar);
                    _mySoldier.Character = c;
                    _mySoldierCharacter = c;                                       
                }
                if(e.KeyChar == 's')
                {
                    LabelField state = new LabelField(_mySoldier.Content);
                    state.Name = _mySoldier.Name;                    
                    state.Location = _mySoldier.Location;
                    state.MatrixIndex = _mySoldier.MatrixIndex;
                    if (_mySoldier.UP != null) state.UP = _mySoldier.UP;
                    if (_mySoldier.DowN != null) state.DowN = _mySoldier.DowN;
                    if (_mySoldier.LefT != null) state.LefT = _mySoldier.LefT;
                    if (_mySoldier.RighT != null) state.RighT = _mySoldier.RighT;
                    _originator.SetState(state);
                    _careTaker.Add(_originator.SaveStateToMemento());
                }


                if (e.KeyChar == 'l' && _careTaker.HowMuchMementos > _count+1)
                {
                    
                    _originator.GetStateFromMemento(_careTaker.GetLast());
                    if (_careTaker.TakingCount < 0) return;

                    
                    LabelField state = _originator.State;
                    _pnlMatrixHolder.Controls.RemoveByKey(_mySoldier.Name);




                    LabelField substitute = _matrix[_mySoldier.MatrixIndex.X, _mySoldier.MatrixIndex.Y];
                    substitute.Character = ' ';
                    _pnlMatrixHolder.Controls.Add(substitute);


                    _mySoldier = state;
                    _matrix[_mySoldier.MatrixIndex.X, _mySoldier.MatrixIndex.Y] = _mySoldier;
                    _pnlMatrixHolder.Controls.RemoveByKey(state.Name);                    
                    _pnlMatrixHolder.Controls.Add(_mySoldier);


                    _count++;
                }
            };

            
        }
        private char ChangeSoldier(LabelField field, char keyChar)
        {            
            if (!int.TryParse(field.Character.ToString(), out int numberOnSoldier))
            {
                MessageBox.Show($"The sign \"{field.Character}\" isn't a number");
                return default(char);
            }
            switch (keyChar)
            {
                case '+':
                    numberOnSoldier++;
                    break;
                case '-':
                    numberOnSoldier--;
                    break;
            }
            if (numberOnSoldier < 0) numberOnSoldier = 9;
            if (numberOnSoldier > 9) numberOnSoldier = 0;
            return numberOnSoldier.ToString()[0];
        }

        private void MoveSoldier(LabelField field)
        {
            if (field != null)
            {
                LabelField fieldUP = null;
                if (field.MatrixIndex.Y - 1 >= 0 && field.MatrixIndex.Y - 1 < _matrix.GetLength(1)) fieldUP = _matrix[field.MatrixIndex.X, field.MatrixIndex.Y - 1];
                LabelField fieldDowN = null;
                if (field.MatrixIndex.Y + 1 >= 0 && field.MatrixIndex.Y + 1 < _matrix.GetLength(1)) fieldDowN = _matrix[field.MatrixIndex.X, field.MatrixIndex.Y + 1];
                LabelField fieldLefT = null;
                if (field.MatrixIndex.X - 1 >= 0 && field.MatrixIndex.X - 1 < _matrix.GetLength(0)) fieldLefT = _matrix[field.MatrixIndex.X - 1, field.MatrixIndex.Y];
                LabelField fieldRighT = null;
                if (field.MatrixIndex.X + 1 >= 0 && field.MatrixIndex.X + 1 < _matrix.GetLength(0)) fieldRighT = _matrix[field.MatrixIndex.X + 1, field.MatrixIndex.Y];

                field.UP = fieldUP;
                field.DowN = fieldDowN;
                field.LefT = fieldLefT;
                field.RighT = fieldRighT;



                field.Character = _mySoldierCharacter;
                _mySoldier.Character = ' ';                
                _mySoldier = field;
            }
        }

        private void CreateMatrix()
        {
            bool setMySoldier = false;
            Char character;
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    character = ' ';
                    if ((i == (_matrix.GetLength(0) + 1) / 2 - 1) && (j == (_matrix.GetLength(1) + 1) / 2 - 1))
                    {
                        character = _mySoldierCharacter;
                        setMySoldier = true;
                    }
                    _matrix[i, j] = new LabelField(new ContentLabel(character, Color.DarkBlue), _labelFieldWidth, _labelFieldHeight);
                    _matrix[i, j].Location = new Point(i * (_matrix[i, j].Width + _labelFieldLocationStep) + _pnlMatrixHolderPadding, j * (_matrix[i, j].Height + _labelFieldLocationStep) + _pnlMatrixHolderPadding);
                    _matrix[i, j].MatrixIndex = new Point(i, j);
                    _matrix[i, j].Name = _matrix[i, j].MatrixIndex.ToString();




                    if (setMySoldier == true)
                    {
                        _mySoldier = _matrix[i, j];
                        setMySoldier = false;
                    }

                    _matrix[i, j].Click += (object sender, EventArgs e) =>
                    {
                        this.MoveSoldier((LabelField)sender);
                    };


                    _pnlMatrixHolder.Controls.Add(_matrix[i, j]);
                }

            }
            AddFriends();
        }

        private void AddFriends()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {

                    LabelField up = null;
                    if ((j - 1) >= 0 && (j - 1) < _matrix.GetLength(1)) up = _matrix[i, j - 1];
                    _matrix[i, j].UP = up;
                    LabelField down = null;
                    if ((j + 1) >= 0 && (j + 1) < _matrix.GetLength(1)) down = _matrix[i, j + 1];
                    _matrix[i, j].DowN = down;
                    LabelField right = null;
                    if ((i + 1) >= 0 && (i + 1) < _matrix.GetLength(0)) right = _matrix[i + 1, j];
                    _matrix[i, j].RighT = right;
                    LabelField left = null;
                    if ((i - 1) >= 0 && (i - 1) < _matrix.GetLength(0)) left = _matrix[i - 1, j];
                    _matrix[i, j].LefT = left;

                }
            }
        }
    }
}
