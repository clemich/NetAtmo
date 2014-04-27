using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAtmo.Entities
{
    public class DeviceList
    {
        public class DefaultAlarm
        {
            public int db_alarm_number { get; set; }
            public bool desactivated { get; set; }
        }

        public class AlarmConfig
        {
            public List<DefaultAlarm> default_alarm { get; set; }
            public List<object> personnalized { get; set; }
        }

        public class DateSetup
        {
            public int sec { get; set; }
            public int usec { get; set; }
        }

        public class DataStore
        {
            [JsonProperty("'")]
            public double Unknown { get; set; }
            public int K { get; set; }
            public int S { get; set; }
            [JsonProperty("a")]
            public double Temperature { get; set; }
            public int b { get; set; }
            public double e { get; set; }
            public int h { get; set; }
        }

        public class LastMarkTime
        {
            public int sec { get; set; }
            public int usec { get; set; }
        }

        public class Place
        {
            public int altitude { get; set; }
            public string bssid { get; set; }
            public string country { get; set; }
            public string geoip_city { get; set; }
            public List<double> location { get; set; }
            public string meteoalarm_area { get; set; }
            public string timezone { get; set; }
            public bool trust_location { get; set; }
        }

        public class Service
        {
            public bool meteo_alarm { get; set; }
        }

        public class Device
        {
            [JsonProperty("_id")]
            public string Id { get; set; }
            public string access_code { get; set; }
            public AlarmConfig alarm_config { get; set; }
            public int battery_rint { get; set; }
            public int battery_vp { get; set; }
            public string co2_abc_status { get; set; }
            public bool co2_calibrating { get; set; }
            public int consolidation_date { get; set; }
            public DateSetup date_setup { get; set; }
            public int firmware { get; set; }
            public List<string> friend_users { get; set; }
            public int hw_version { get; set; }
            public int last_alarm_stored { get; set; }
            public Dictionary<string, DataStore> last_data_store { get; set; }
            public int last_event_stored { get; set; }
            public LastMarkTime last_mark_time { get; set; }
            public List<int> last_marks { get; set; }
            public int last_status_store { get; set; }
            public int mark { get; set; }
            [JsonProperty("module_name")]
            public string ModuleName { get; set; }
            public List<string> modules { get; set; }
            public string netcom_transport { get; set; }
            public Place place { get; set; }
            public bool public_ext_data { get; set; }
            public int rf_amb_status { get; set; }
            public Service service { get; set; }
            [JsonProperty("station_name")]
            public string StationName { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            public bool update_device { get; set; }
            public int wifi_status { get; set; }
            public string streaming_key { get; set; }
            public List<string> data_type { get; set; }
        }

        public class Module
        {
            [JsonProperty("_id")]
            public string Id { get; set; }
            public int last_event_stored { get; set; }
            public string main_device { get; set; }
            [JsonProperty("module_name")]
            public string ModuleName { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("firmware")]
            public int Firmware { get; set; }
            public int last_message { get; set; }
            public int last_seen { get; set; }
            public int rf_status { get; set; }
            public int battery_vp { get; set; }
            public List<string> data_type { get; set; }
            public bool? manual_pairing { get; set; }
        }

        public class BodyHelper
        {

            [JsonProperty("devices")]
            public List<Device> Devices { get; set; }
            [JsonProperty("modules")]
            public List<Module> Modules { get; set; }
        }

        public string status { get; set; }
        [JsonProperty("body")]
        public BodyHelper Body { get; set; }
        public double time_exec { get; set; }
        public int time_server { get; set; }
    }
}
