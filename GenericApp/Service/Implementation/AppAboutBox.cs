using MpdBaileyTechnology.GenericApp.Service.Services;
using MpdBaileyTechnology.GenericApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Service.Implementation
{
    public class AppAboutBox : IAboutBox
    {
        public void Show()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}