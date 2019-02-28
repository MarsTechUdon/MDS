
$(function () {
    $.validator.setDefaults({
        errorClass: 'help-block',
        highlight: function (element) {
            $(element)
                .closest('.form-group')
                .addClass('has-error');
        },
        unhighlight: function (element) {
            $(element)
                .closest('.form-group')
                .removeClass('has-error');
        },
        errorPlacement: function (error, element) {
            if (element.prop('type') === 'checkbox') {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $("#Firstlg").validate({
        rules: {
            OldPassword: {
                required: true
            },
            NewPassword: {
                required: true,
                minlength: 8,
                pwcheck: true
               
            },
            CrmNewPassword: {
                required: true,
                equalTo: '#NewPassword'
            }
        },
        messages: {
            OldPassword: {
                required: 'กรุณากรอกรหัสผ่านเดิม'
            },
            NewPassword: {
                required: 'กรุณากรอกรหัสผ่านใหม่',
                minlength: 'รหัสผ่านต้องไม่ต่ำกว่า 8',
                pwcheck: "กรุณากรอกรหัสผ่านที่มี ตัวอักษร และ ตัวเลข"
                
            },
            CrmNewPassword: {
                required: 'กรุณากรอกรหัสผ่านอีกครั้ง',
                equalTo: 'รหัสผ่านต้องเหมือนกัน'
            },

        }
    });
    $.validator.addMethod("pwcheck", function (value) {
        return /[a-z]/.test(value) // has a lowercase letter
            && /[0-9]/.test(value)  // consists of only these
    });
    $("#Schoolinfo").validate({
        rules: {
            sendemailuser: {
                required: true

            },
            sendemailpass: {
                required: true

            },
            CrmPassword: {
                required: true,
                equalTo: '#sendemailpass'
            }
        },
        messages: {
            sendemailuser: {
                required: 'กรุณาอีเมล'

            },
            sendemailpass: {
                required: 'กรุณากรอกรหัสผ่านใหม่'

            },
            CrmPassword: {
                required: 'กรุณากรอกรหัสผ่านอีกครั้ง',
                equalTo: 'รหัสผ่านต้องเหมือนกัน'
            }

        }
    });
});