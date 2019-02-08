
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
    $.validator.addMethod("valueNotEquals",
        function (value, element, arg) {
            return arg !== value;
        }, "Value must not equal arg.");

    $("#SearchCourse").validate({
        rules: {
            courseid: {
                required: true

            },
            bookingname: {
                required: true,
                minlength: 5
            },
             gear: {
                required: true

            }
        },
        messages: {
            courseid: {
                required: 'กรุณาเลือกหลักสูตร'
            },
            bookingname: {
                required: 'กรุณากรอกชื่อนักเรียน/ชื่อจอง',
                minlength: 'ความยาวต้องไม่ต่ำกว่า 5 ตัวอักษร'

            },
            gear: {
                required: 'กรุณาเลือกเกียร์'
            }

        }
    });
    //$("#AddEventDropfrm").validate({
    //    rules: {
    //        teacherDrop: {
    //            required: true

    //        }
    //    },
    //    messages: {
    //        teacherDrop: {
    //            required: 'กรุณาเลือกครูผู้สอน'
    //        }

    //    },
    //    submitHandler: function (form) {

    //    }
    //});
    //$("#AddEventAddfrm").validate({
    //    rules: {
    //        subjectAdd: {
    //            valueNotEquals: ""
    //        },
    //        teacherAdd: {
    //            valueNotEquals: ""
    //        }
            
    //    },
    //    messages: {
    //        subjectAdd: {
    //            valueNotEquals: 'กรุณาเลือกวิชา'
    //        },
    //        teacherAdd: {
    //            valueNotEquals: 'กรุณาเลือกครูผู้สอน'
    //        }

    //    }
    //    ,
    //      submitHandler: function (form) {
    //        if (submitbutton_click()) {
    //            form.submit();
    //        }
    //        //return true;
    //    }
    //});
    //$("#EditEventfrm").validate({
    //    rules: {
    //        subjectEdit: {
    //            required: true
    //        },
    //        teacherEdit: {
    //            required: true

    //        }
    //    },
    //    messages: {
    //        subjectEdit: {
    //            required: 'กรุณาเลือกวิชา'
    //        },
    //        teacherEdit: {
    //            required: 'กรุณาเลือกครูผู้สอน'
    //        }

    //    }
    //});
    $("#SentToCreateStudentfrm").validate({
        rules: {
            Studenttype: {
                required: true
            }
        },
        messages: {
            Studenttype: {
                required: 'กรุณาเลือกบัตร'
            }

        }
    });
});