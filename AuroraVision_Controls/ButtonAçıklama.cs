using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMI;
using System.Drawing.Drawing2D;


namespace AuroraVision_Controls
{
    //Aurora tarafında hangi sekme altında göstermek istediğimizi belirtiyoruz.
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    //Aurora tarafında göstereceğimiz iconu belirtiyoruz.
    //Sadece C# tarafında görselin `Property` kısmında `Build Action` özelliğini 
    //`Embedded Resource` olarak ayarlıyoruz.
    [ToolboxBitmap(typeof(CameraContol), "Icons.3d-camera.png")]
    //Oluşturduğumuz komponentin aurora tarafında göstermek istediğimiz açıklama kısmı
    [Description("Kamerayı hareket ettirir.")]
    public partial class ButtonAçıklama: UserControl
    {
        //Aurora tarafından gelen veriyi almak için değişken oluşturuyoruz
        Boolean _isTrue = true;
        int _min;
        int _max;

        public ButtonAçıklama()
        {
            InitializeComponent();

            //Butonun DockStyle ayarını fill yapıyoruz.Bu sayede içerisinde olduğu
            //kontrol veya panele sığacak şekilde konumlanıyor.
            //Bu sayede panelin büyüklüğüne göre dinamik olarak kendi boyutu değişiyor.
            //button.Dock = DockStyle.Fill;

            //Butonun arkaplan rengini değiştiriyoruz
            button.BackColor = Color.White;

            //Butonun text rengini değiştiriyoruz
            button.ForeColor = Color.Black;

            //Başka aksiyonlar sonucu butonun görünürlüğünü değiştirebiliriz
            button.Visible = true;

            //Buton metninin konumlandıırlması
            button.TextAlign = ContentAlignment.MiddleCenter;

            //Mouse imleci butonun üzerine geldiği zaman imleci değiştiriyoruz
            button.Cursor = Cursors.Hand;

            
            
        }

        //Giriş portu olduğunu belirtiyoruz
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        Boolean isTrue
        {
            //Burada get ve set metodları ile `_isTrue` değişkenini kullanarak
            //veriyi içeriye aktarıyoruz.
            get 
            { 
                return _isTrue; 
            }
            set 
            { 
                _isTrue = value; 
            }
        }

        //Hidden input ayarladığımız zaman Aurora tarafında bu portu 
        //istediğimiz zaman açıp/kapatabiliyoruz.
        [HMI.HMIPortProperty(HMI.HMIPortDirection.HiddenInput)]
        int min
        {
            get { return _min; }
            set { 
                _min = value; 
            }

        }

        //Aynı şekilde bu fonksiyonu kullanarak veriyi dışarıya açmış oluyoruz.
        [HMIPortProperty(HMIPortDirection.Output)]
        int max
        {
            get { return _max; }
            set { _max = value; }
        }
    }
}
