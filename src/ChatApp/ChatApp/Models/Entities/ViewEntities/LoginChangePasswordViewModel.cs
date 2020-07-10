﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class LoginChangePasswordViewModel
    {
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        [DisplayName("現在のパスワード")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("新しいパスワード")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("新しいパスワード（確認用）")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; }
    }
}
