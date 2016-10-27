function CheckMail(mail) {
    var reg = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (reg.test(mail)) {
        return true;
    }
    else {
        return false;
    }
}

function CheckPwd(pwd) {
    var reg = /^[a-zA-Z0-9]*$/;
    if (reg.test(pwd)) {
        return true;
    } else {
        return false;
    }
}