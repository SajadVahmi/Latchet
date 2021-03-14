using Dapper;
using Latchet.Infrastructures.Events.Outbox;
using Latchet.Utilities.Configurations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Persistence.Sql.Command.OutBoxEventItems
{
    public class SqlOutBoxEventItemRepository : IOutBoxEventItemRepository
    {
        private readonly LatchetConfigurations _configurations;

        public SqlOutBoxEventItemRepository(LatchetConfigurations configurations)
        {
            _configurations = configurations;
        }
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100)
        {
            using var connection = new SqlConnection(_configurations.PullingPublisher.SqlOutBoxEvent.ConnectionString);
            string query = string.Format(_configurations.PullingPublisher.SqlOutBoxEvent.SelectCommand, maxCount);
            var result = connection.Query<OutBoxEventItem>(query).ToList();
            return result;
        }
        public void MarkAsRead(List<OutBoxEventItem> outBoxEventItems)
        {
            string idForMark = string.Join(',', outBoxEventItems.Where(c => c.IsProcessed).Select(c => c.OutBoxEventItemId).ToList());
            if (!string.IsNullOrWhiteSpace(idForMark))
            {
                using var connection = new SqlConnection(_configurations.PullingPublisher.SqlOutBoxEvent.ConnectionString);
                string query = string.Format(_configurations.PullingPublisher.SqlOutBoxEvent.UpdateCommand, idForMark);
                connection.Execute(query);
            }
        }
    }
}
