function ValidateFile(value) {

    var file = getNameFromPath($(value).val());
    if (file != null) {
        var extension = file.substr((file.lastIndexOf('.') + 1));
        switch (extension) {
            case 'jpg':
            case 'jpeg':
            case 'png':
            case 'pdf':
                flag = true;
                break;
            default:
                flag = false;
        }
    }

    if (flag == false) {

        var str = value.name;
        var res = str.split("_");
        var data = "_val" + res[1];
        $("#" + data).text("You can upload only jpg, jpeg, png, pdf extension file Only");
        $("#" + value.name).val('');
        return false;
    }
    else {
        var size = ValidateFileSize(value);
        var str = value.name;
        var res = str.split("_");
        var data = "_val" + res[1];
        if (size > 1) {
            $("#" + data).text("You Can Upload file Size Up to 1 MB.");
            $("#" + value.name).val('');
        }
        else {
            $("#" + data).text("");
        }
    }
} 
function getNameFromPath(strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}  
function ValidateFileSize(fileid) {
    try {
        var fileSize = 0;
        if (navigator.userAgent.match(/msie/i)) {
            var obaxo = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = $("#" + fileid)[0].value;
            var objFile = obaxo.getFile(filePath);
            var fileSize = objFile.size;
            fileSize = fileSize / 1048576;
        }
        else {
            fileSize = $(fileid)[0].files[0].size
            fileSize = fileSize / 1048576;
        }

        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}  