

function initCardReader(page) {
    changeCSSDisplay('loading1', 'block');
    checkDriver(function (hasDriver, isUpdated, usingVer) {
        if (hasDriver && isUpdated) {
            document.getElementById('resultText').innerHTML = 'กำลังอ่านบัตร';

            document.getElementById('driverDL').style.display = 'none';

            if ((page == 'tchcheckin' || page == 'classin')) {
                //quick read
                readCard(1, function (resOBJ) {
                    setData(resOBJ, page);
                });
            }
            else {
                //read
                readCard(0, function (resOBJ) {
                    setData(resOBJ, page);
                });
            }
        }

        if (hasDriver && !isUpdated) {
            fadeCSS('cnotice', 1);
            document.getElementById('resultText').innerHTML = '<p>มี Driver Version ใหม่ (0.8)</p>';
            document.getElementById('resultText').innerHTML += '<p>คุณกำลังใช้ version ' + usingVer + ' </p>';
            document.getElementById('resultText').innerHTML += '<br><a href="../../Content/Downloads/Driver/DLTNatID.0.8.zip">คู่มือช่วยเหลือ</a>';
            if (usingVer < 0.5) {
                document.getElementById('resultText').innerHTML += '<b style="color:red">* หาก Version ต่ำกว่า 0.5 ก่อนอัพเดตจะต้องถอนการติดตั้งตัวเก่าก่อน</b>';
                document.getElementById('resultText').innerHTML += '<b style="color:red">* Windows ที่ตำกว่า 10 อาจจะพบปัญหาในการลง</b>';

            }
            document.getElementById('driverDL').style.display = 'block';
            //read
            readCard(0, function (resOBJ) {
                setData(resOBJ, page);
            });
        }
        else if (!hasDriver) {
            changeCSSDisplay('loading1', 'none');
            fadeCSS('cnotice', 1);
            document.getElementById('resultText').innerHTML = 'ไม่พบ Driver บนเครื่องนี้';
            document.getElementById('resultText').innerHTML += '<br><a href="../../Content/Downloads/Driver/DLTNatID.0.8.zip">คู่มือช่วยเหลือ</a>';
            document.getElementById('driverDL').style.display = 'block';
        }

    });
}

function changeCSSDisplay(id, styl) {
    document.getElementById(id).style.display = styl;
}
function fadeCSS(id, opa) {
    document.getElementById(id).style.opacity = opa;
}

function checkDriver(callback) {
    resultbool = false;
    isUpdated = false;

    httpGetAsync("http://localhost:21998/info", function (resStatus, resText) {
        var version = 'none';
        if (resText != null) {
            var resObj = JSON.parse(resText);
            if (resObj) {
                if (resObj.result == 'ok') {
                    resultbool = true;
                    //appInsights.trackEvent("Scan ID Card", { "Version": resObj.version });
                    if (resObj.version >= 0.8) {
                        isUpdated = true;
                    }
                    version = resObj.version;
                }
            }
        }
        else {
            console.log('is null');
        }
        callback(resultbool, isUpdated, version)
    });

}

function readCard(mode, callback) {
    var url = 'http://localhost:21998/readCard';
    if (mode == 1) { url = 'http://localhost:21998/readCardNoPhoto'; console.log('reading real fast with style!!') }
    else { console.log('reading slowly'); }

    httpGetAsync(url, function (resStatus, resText) {
        if (resText != null) {
            var resObj = JSON.parse(resText);
            if (resObj) {
                if (resObj.result == 'ok') {
                    callback(resObj);
                }
                else if (resObj.result == 'read initiate failed') {
                    changeCSSDisplay('loading1', 'none');
                    fadeCSS('cnotice', 1);
                    document.getElementById('resultText').innerHTML = 'ไม่พบบัตรประชาชน <br>FailedCode: ' + resObj.ecode;
                    if (resObj.ecode == '256' && resObj.devices.length == 2) {
                        document.getElementById('resultText').innerHTML += '<br>มีเครื่องอ่านบัตรเชื่อมต่อหลายเครื่อง กรุณาถอดให้เหลือ 1 เครื่อง';
                    }
                    else if (resObj.ecode == '256') {
                        document.getElementById('resultText').innerHTML += '<br><b style="color:red">กรุณาตรวจสอบว่าเสียบบัตรถูกด้านหรือไม่</b>';
                        document.getElementById('resultText').innerHTML += '<br><b style="color:red">หรือทดลองใช้ยางลบ เช็ดทำความสะอาดชิฟก่อนอ่านอีกครั้ง</b>';
                    }
                    else if (resObj.ecode == '1') {
                        document.getElementById('resultText').innerHTML += '<br><b style="color:red">ไม่ใช่บัตรประชาชน หรือเป็นรุ่นที่ไม่รองรับ</b>';
                    }
                }
                else if (resObj.result == 'error phrasing data') {
                    changeCSSDisplay('loading1', 'none');
                    fadeCSS('cnotice', 1);
                    document.getElementById('resultText').innerHTML = 'ไม่พบบัตรประชาชน <br>ErrMsg: <b style="color:red">' + resObj.ecode + '</b>';
                }
                else if (resObj.result == 'error at read') {
                    changeCSSDisplay('loading1', 'none');
                    fadeCSS('cnotice', 1);

                    if (resObj.ecode != "Could not find any Smartcard reader.") {
                        document.getElementById('resultText').innerHTML = 'ไม่พบบัตรประชาชน <br>ErrMsg: ' + resObj.ecode;
                    }
                    else {
                        document.getElementById('resultText').innerHTML = '<b style="color:red">ไม่พบเครื่องอ่านบัตรประชาชน</b>';
                    }
                }
                else {
                    changeCSSDisplay('loading1', 'none');
                    fadeCSS('cnotice', 1);
                    document.getElementById('resultText').innerHTML = 'ไม่พบบัตรประชาชน <br>ErrMsg: ' + resObj.result;
                }
                //fail
            }
        }
        else {
            console.log('reader error');
        }
    });

}

function setData(resObj, page) {
    console.log(resObj.nat_id, page)
    document.getElementById("fname").value = resObj.fname_th;
    document.getElementById("lname").value = resObj.sname_th;
    document.getElementById("fname_en").value = resObj.fname_en;
    document.getElementById("lname_en").value = resObj.sname_en;
    document.getElementById("idno").value = resObj.nat_id;
    document.getElementById("national").value = 'ไทย';
    document.getElementById("gender").value = resObj.gender;
    document.getElementById("district").value = resObj.address_tumbol;
    document.getElementById("amphoe").value = resObj.address_amphor;
    document.getElementById("address").value = resObj.address_no + ' ' + resObj.address_moo + ' ' + resObj.address_trok
        + ' ' + resObj.address_soi + ' ' + resObj.address_road + ' ' + resObj.address_tumbol + ' ' + resObj.address_amphor
        + ' ' + resObj.address_provinice; 
    document.getElementById("province").value = resObj.address_provinice;
    document.getElementById("dateofbirth").value = resObj.birthdate;
    document.getElementById("titleT").value = resObj.title_th;
    document.getElementById("titleE").value = resObj.title_en;
    document.getElementById("photo").src = 'data:image/png;base64, ' + resObj.photo;
    document.getElementById("photo_temp").value = resObj.photo;
    var calage = resObj.birthdate;
    calage = calage.split("-");
    var dayborn = calage[calage.length - 1]
    var monthborn = calage[calage.length - 2]
    var calage = calage[calage.length - 3]
    if (calage[1] == "00") {
        calage[1] == "01";
        document.getElementById("dateofbirth").value = calage[0] + '-' + calage[1] + '-' + calage[2];
    }
    if (calage[2] == "00") {
        calage[2] == "01";
        document.getElementById("dateofbirth").value = calage[0] + '-' + calage[1] + '-' + calage[2];
    }
    var currentyear = (new Date()).getFullYear();
    var currentmonth = (new Date()).getMonth() + 1;
    var currentday = (new Date()).getDate();
    if (currentyear < calage) { calage = calage - 543; }
    calage = currentyear - calage;
    if (currentmonth < monthborn) { calage = calage - 1 }
    else if (currentday < dayborn && currentmonth <= monthborn) { calage = calage - 1 }

    //var id = document.getElementById("permitid").value;
    //if (id == 2 || id == 7 || id == 8 || id == 4 || id == 11 || id == 12) {
    //    if (calage < 18) {
    //        $("#save").attr('disabled', 'disabled');
    //        document.getElementById("age").value = calage;
    //        fadeCSS('cnotice', 0);
    //        changeCSSDisplay('loading1', 'none');
    //        return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 18 ปีบริบูรณ์");


    //    } else {
    //        $("#save").removeAttr('disabled');

    //    }

    //} else if (id == 5 || id == 13 || id == 6) {
    //    if (calage < 20) {
    //        $("#save").attr('disabled', 'disabled');
    //        document.getElementById("age").value = calage;
    //        fadeCSS('cnotice', 0);
    //        changeCSSDisplay('loading1', 'none');
    //        return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 20 ปีบริบูรณ์");


    //    } else {
    //        $("#save").removeAttr('disabled');
    //    }
    //} else if (id == 3 || id == 9 || 10) {
    //    if (calage < 15) {
    //        $("#save").attr('disabled', 'disabled');
    //        document.getElementById("age").value = calage;
    //        fadeCSS('cnotice', 0);
    //        changeCSSDisplay('loading1', 'none');
    //        return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 15 ปีบริบูรณ์");

    //    } else {
    //        $("#save").removeAttr('disabled');
    //    }

    //}
    document.getElementById("age").value = calage;
    fadeCSS('cnotice', 0);
    changeCSSDisplay('loading1', 'none');
}

//function setData(resObj, page) {
//    console.log(resObj.nat_id, page)
//    if (page == 'attend') {
//        document.getElementById("natid").value = resObj.nat_id;
//        document.getElementById('fmessage').innerHTML = '<a href="#" onclick="natinsert()"><h2>สวัสดีคุณ ' + request.data.fname_th + ' กรุณาเริ่มการสแกนลายนิ้วมือ</h2></a>';
//        document.getElementById('fullname').value = resObj.fname_th + ' ' + resObj.sname_th;
//        changeCSSDisplay('loading1', 'none');
//    }
//    else if (page == 'tchcheckin') {//quick read
//        try { tphoto = resObj.photo; console.log('set'); console.log(tphoto); }
//        catch (e) { console.log('cannot set'); }

//        changeCSSDisplay('loading1', 'none');
//        teacherNatId = resObj.nat_id;
//        teacherFullName = resObj.fname_th + ' ' + resObj.sname_th;
//        document.getElementById('readresult').innerHTML = 'อ่านบัตรประชาชนสำเร็จ';
//        changeCSSDisplay('insnatBTN', 'none');
//        changeCSSDisplay('insnat', 'none');
//        changeCSSDisplay('insfin', 'block');
//        changeCSSDisplay('insfinBTN', 'inline');
//    }
//    else if (page == 'classin') {//quick read
//        document.getElementById('std_natid').value = resObj.nat_id;
//        document.getElementById('fullname').value = resObj.fname_th + ' ' + resObj.sname_th;
//        natCurrentId = resObj.nat_id;
//        natCurrentFullName = resObj.fname_th + ' ' + resObj.sname_th;
//        changeCSSDisplay('loading1', 'none');
//        natinsert();
//    }
//    else if (page == 'teacher') {
//        document.getElementById("fullname").value = resObj.fname_th + ' ' + resObj.sname_th;
//        document.getElementById("fullname_en").value = resObj.fname_en + ' ' + resObj.sname_en;
//        document.getElementById("idno").value = resObj.nat_id;
//        document.getElementById("national").value = 'ไทย';
//        document.getElementById("sex").value = resObj.gender;
//        document.getElementById("address").innerHTML = resObj.address_no + ' ' + resObj.address_moo + ' ' + resObj.address_trok + ' ' + resObj.address_soi + ' ' + resObj.address_road + ' ' + resObj.address_tumbol + ' ' + resObj.address_amphor + ' ' + resObj.address_provinice;
//        document.getElementById("title").value = resObj.title_th;
//        document.getElementById("title_en").value = resObj.title_en;
//        document.getElementById("imgprvw").src = 'data:image/png;base64, ' + resObj.photo;
//        document.getElementById("photo_temp").value = resObj.photo;
//        var calage = resObj.birthdate;
//        calage = calage.split("-");
//        calage = calage[calage.length - 3]

//        var currentyear = (new Date()).getFullYear();
//        if (currentyear < calage) { calage = calage - 543; }
//        calage = currentyear - calage;
//        document.getElementById("age").value = calage;
//        fadeCSS('cnotice', 0);
//        changeCSSDisplay('loading1', 'none');
//    }
//    else {
//        var checkselectedPermite = document.getElementById("permitid");
//        var selectedValue = checkselectedPermite.options[checkselectedPermite.selectedIndex].value;
//        if (selectedValue == "" || selectedValue == null) {
//            alert("กรุณาเลือกสูตรก่อนทำการใส่วันที่");
//            fadeCSS('cnotice', 0);
//            changeCSSDisplay('loading1', 'none');

//        } else {
//            document.getElementById("fname").value = resObj.fname_th;
//            document.getElementById("lname").value = resObj.sname_th;
//            document.getElementById("fname_en").value = resObj.fname_en;
//            document.getElementById("lname_en").value = resObj.sname_en;
//            document.getElementById("idno").value = resObj.nat_id;
//            document.getElementById("national").value = 'ไทย';
//            document.getElementById("gender").value = resObj.gender;
//            document.getElementById("address").value = resObj.address_no + ' ' + resObj.address_moo + ' ' + resObj.address_trok + ' ' + resObj.address_soi + ' ' + resObj.address_road + ' ' + resObj.address_tumbol + ' ' + resObj.address_amphor;
//            document.getElementById("province").value = resObj.address_provinice;
//            document.getElementById("dateofbirth").value = resObj.birthdate;
//            document.getElementById("title").value = resObj.title_th;
//            document.getElementById("title_en").value = resObj.title_en;
//            document.getElementById("photo").src = 'data:image/png;base64, ' + resObj.photo;
//            document.getElementById("photo_temp").value = resObj.photo;
//            var calage = resObj.birthdate;
//            calage = calage.split("-");
//            var dayborn = calage[calage.length - 1]
//            var monthborn = calage[calage.length - 2]
//            var calage = calage[calage.length - 3]
//            if (calage[1] == "00") {
//                calage[1] == "01";
//                document.getElementById("dateofbirth").value = calage[0] + '-' + calage[1] + '-' + calage[2];
//            }
//            if (calage[2] == "00") {
//                calage[2] == "01";
//                document.getElementById("dateofbirth").value = calage[0] + '-' + calage[1] + '-' + calage[2];
//            }
//            var currentyear = (new Date()).getFullYear();
//            var currentmonth = (new Date()).getMonth() + 1;
//            var currentday = (new Date()).getDate();
//            if (currentyear < calage) { calage = calage - 543; }
//            calage = currentyear - calage;
//            if (currentmonth < monthborn) { calage = calage - 1 }
//            else if (currentday < dayborn && currentmonth <= monthborn) { calage = calage - 1 }

//            var id = document.getElementById("permitid").value;
//            if (id == 2 || id == 7 || id == 8 || id == 4 || id == 11 || id == 12) {
//                if (calage < 18) {
//                    $("#save").attr('disabled', 'disabled');
//                    document.getElementById("age").value = calage;
//                    fadeCSS('cnotice', 0);
//                    changeCSSDisplay('loading1', 'none');
//                    return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 18 ปีบริบูรณ์");


//                } else {
//                    $("#save").removeAttr('disabled');

//                }

//            } else if (id == 5 || id == 13 || id == 6) {
//                if (calage < 20) {
//                    $("#save").attr('disabled', 'disabled');
//                    document.getElementById("age").value = calage;
//                    fadeCSS('cnotice', 0);
//                    changeCSSDisplay('loading1', 'none');
//                    return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 20 ปีบริบูรณ์");


//                } else {
//                    $("#save").removeAttr('disabled');
//                }
//            } else if (id == 3 || id == 9 || 10) {
//                if (calage < 15) {
//                    $("#save").attr('disabled', 'disabled');
//                    document.getElementById("age").value = calage;
//                    fadeCSS('cnotice', 0);
//                    changeCSSDisplay('loading1', 'none');
//                    return confirm("อายุผู้สมัครต้องไม่น้อยกว่า 15 ปีบริบูรณ์");

//                } else {
//                    $("#save").removeAttr('disabled');
//                }

//            }
//            document.getElementById("age").value = calage;
//            fadeCSS('cnotice', 0);
//            changeCSSDisplay('loading1', 'none');

//        }
//    }


//}

function httpGetAsync(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("GET", theUrl, true);
    xmlHttp.send(null);
}

function httpPostAsync(theUrl, params, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("Post", theUrl, true);
    xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlHttp.send(params);
}

function httpPostJSONAsync(theUrl, json, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("Post", theUrl, true); // true for asynchronous 
    xmlHttp.setRequestHeader("Content-Type", "application/json");
    xmlHttp.send(JSON.stringify(json));
}
