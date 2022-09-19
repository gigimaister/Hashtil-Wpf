using Hashtil_Jobs_For_Drivers.DataBase;
using Hashtil_Jobs_For_Drivers.Models;
using Microsoft.Maps.MapControl.WPF;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.UserControlScreens
{
    /// <summary>
    /// Interaction logic for CagesOnMapControl.xaml
    /// </summary>
    public partial class CagesOnMapControl : UserControl
    {
        ObservableCollection<CageAudit> cageAudits = new ObservableCollection<CageAudit>();
        private static System.Timers.Timer timer;
        DBManagerOld dbManagerOld = new DBManagerOld();
        public CagesOnMapControl()
        {
            InitializeComponent();
            PopulateMainGrid();
            // For aoto refresh
            timer = new System.Timers.Timer();
            timer.Interval = 120000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        // Auto Refresh
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            PopulateMainGrid();
        }


        // Fill Data Grid With Data
        private async void PopulateMainGrid()
        {
            try
            {

                await Dispatcher.InvokeAsync(async () =>
                {
                    
                    var MainCageTable = await CageAuditTableJson.CageAuditFetchJson(1);
                    sfdg.ItemsSource = MainCageTable;


                    myMap.Children.Clear();

                    var CageAuditCalcTable = await CageAuditTableJson.CageAuditFetchJson(2);
                                      

                    if (cageAudits.Count > 0)
                    {
                        SetPinsOnMap(cageAudits);
                    }
                    else
                    {

                        SetPinsOnMap(CageAuditCalcTable);

                    }


                }
           );
                await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    // Loading Spinner Off
                    LSpinner.Visibility = Visibility.Hidden;
                }));
                
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void SetPinsOnMap(ObservableCollection<CageAudit> cageAudit)
        {

            this.Dispatcher.BeginInvoke(new Action(() =>
            {

                foreach (var cage in cageAudit)
                {


                    Location location = new Location(cage.Lan, cage.Lon);

                    Pushpin pin = new Pushpin
                    {
                        Location = (location)



                    };
                    ToolTipService.SetToolTip(pin, "שם לקוח: " + cage.Cx + "\n" + "נהג: " + cage.User + "\n" + "מצב: " + cage.Status + "\n" + "כמות כלובים: " + cage.NumOfCage+"\n"
                        +"תאריך: "+cage.Date.ToString("dd/MM/yyyy") + "\n"+"שעה: "+cage.Time);

                    myMap.Children.Add(pin);

                }
            }));
        }

        // Buttons
        #region Buttons
        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sfdg.PrintSettings = new PrintSettings();
                sfdg.PrintSettings.PrintFlowDirection = (FlowDirection)1;
                //sfdg.FlowDirection= (FlowDirection)1;
                sfdg.ShowPrintPreview();
            }
            catch
            {

            }
           
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            {
                PopulateMainGrid();
            }
        }

        // Change Map To Satterlite Or Regular
        private void btnchange_map_mode_Click(object sender, RoutedEventArgs e)
        {
            if (myMap.Mode is RoadMode)
            {
                //Set the map mode to Aerial with labels
                myMap.Mode = new AerialMode(true);
            }
            else
            {
                myMap.Mode = new RoadMode();
            }
        }
        #endregion

        // Search Box Text Change
        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.sfdg.SearchHelper.AllowFiltering = true;
            this.sfdg.SearchHelper.Search(TBox.Text);
        }


    }
}
