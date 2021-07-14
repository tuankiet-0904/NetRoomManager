using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongNet.DAL
{
    public class ProcessClient
    {
        static private ProcessClient _instance;
        static public ProcessClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProcessClient();
                return _instance;
            }
            private set { }
        }
        public List<DTO.Client> LoadAllClients()
        {
            using (QuanLyPhongNetDB db = new QuanLyPhongNetDB())
            {
                List<DTO.Client> list = new List<DTO.Client>();
                list = db.Clients.Select(client => new DTO.Client
                {
                    ClientName = client.ClientName,
                    GroupClientName = client.GroupClientName,
                    Status = client.StatusClient,
                    Note = client.Note
                }).ToList();
                return list;
            }

        }
        public void InsertClient(string clientName, string groupClientName, string note)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                objWriter.Clients.Add(new Client
                {
                    ClientName = clientName,
                    GroupClientName = groupClientName,
                    StatusClient = "DISCONNECT",
                    Note = note
                });
                objWriter.SaveChanges();
            }
        }

        public void UpdateClient(string name, string groupClientName, string note, string status)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                Client objUpdate;
                objUpdate = objWriter.Clients.FirstOrDefault(x => x.ClientName.Equals(name));
                objUpdate.ClientName = name;
                objUpdate.GroupClientName = groupClientName;
                objUpdate.StatusClient = status;
                objUpdate.Note = note;
                objWriter.SaveChanges();
            }
        }
        public void DeleteClient(string clientName)
        {
            using (QuanLyPhongNetDB objWriter = new QuanLyPhongNetDB())
            {
                var objDelete = objWriter.Clients.Single(x => x.ClientName.Equals(clientName));
                objWriter.Clients.Remove(objDelete);
                objWriter.SaveChanges();
            }
        }
    }
}
