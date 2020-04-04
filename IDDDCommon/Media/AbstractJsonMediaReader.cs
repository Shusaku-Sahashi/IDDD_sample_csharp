using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace IDDDCommon.Media
{
    internal abstract class AbstractJsonMediaReader : IDisposable
    {
        private JsonDocument Representation { get; set; }
        private JsonElement RootElement { get; set; }

        protected AbstractJsonMediaReader(string message)
        {
            this.Representation = JsonDocument.Parse(message);
            this.RootElement = this.Representation.RootElement;
        }

        protected JsonElement ElementOf(params string[] keys) => this.RootElement.ToNavigateElement(keys);
        protected string StringValue(params string[] keys) => this.RootElement.StringValue(keys);
        protected float? FloatValue(params string[] keys) => this.RootElement.FloatValue(keys);
        protected long? LongValue(params string[] keys) => this.RootElement.LongValue(keys);
        protected DateTime? DateTimeValue(params string[] keys) => this.RootElement.DateTimeValue(keys);

        #region Dispose 
        public void Dispose()
        {
            this.Representation.Dispose();
        }
        #endregion
    }
}