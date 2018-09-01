﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PasswordFilterPS
{
    [Cmdlet(VerbsCommon.Add, "PasswordToStore")]
    public class AddPasswordToStore : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true), ValidateNotNullOrEmpty]
        public string Password { get; set; }


        [Parameter(Mandatory = false, Position = 2, ValueFromPipeline = false, ValueFromPipelineByPropertyName = true)]
        public bool Normalize { get; set; }

        protected override void BeginProcessing()
        {
            Global.OpenExistingDefaultOrThrow();

            Global.Store.StartBatch();
            base.BeginProcessing();
        }

        protected override void EndProcessing()
        {
            int added = 0;
            int discarded = 0;

            Global.Store.EndBatch(ref added, ref discarded);
            base.EndProcessing();
        }

        protected override void ProcessRecord()
        {
            Global.Store.AddPasswordToStore(this.Password, this.Normalize);
        }
    }
}
