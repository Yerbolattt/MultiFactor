﻿namespace MultiFactor.Data.Models
{
    using MultiFactor.Data.Common.Models;

    public class Password : BaseModel<int>
    {
        public string Content { get; set; }

        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
