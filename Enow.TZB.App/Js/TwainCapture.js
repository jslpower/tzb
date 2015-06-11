var CaptureUploadInfo = {"Flag":"0","FilePath":"","Msg":""};
var CaptureTmpFilePath = "d:\\CaptureTmp\\";
var html = [];
var Capture = {
    SelectSource: function () {
        try {
            var CaptureOcx = document.getElementById("CaptureOcx");
            CaptureOcx.Init();
            CaptureOcx.SelectSource();
        } catch (e) {
            alert("err");
        }
    },
    Scan: function () {//扫描
        this.Scan("0", 0, 0, "W");
    },
    Scan: function (IsAutoThumbnail, ThumbnailWidth, ThumbnailHeight, ThumbnailMode) {
        /*
        IsAutoThumbnail:是否自动缩略，0：否 1：是
        ThumbnailWidth:宽
        ThumbnailHeight：高
        ThumbnailMode:生成缩略图的方式 
        HW:指定高宽缩放（可能变形）
        W:指定宽，高按比例
        H:指定高，宽按比例
        Cut:指定高宽裁减（不变形）
        */
        try {
            var CaptureOcx = document.getElementById("CaptureOcx");
            CaptureOcx.Init();
            CaptureOcx.FileSavePath = CaptureTmpFilePath;
            CaptureOcx.IsAutoThumbnail = IsAutoThumbnail;
            CaptureOcx.ThumbnailWidth = ThumbnailWidth;
            CaptureOcx.ThumbnailHeight = ThumbnailHeight;
            CaptureOcx.ThumbnailMode = ThumbnailMode;

            CaptureOcx.Scan();
        } catch (e) {
            alert(e.Message);
        }
    },
    ScanFileArr: function () {//扫描后的原文件
        try {
            var CaptureOcx = document.getElementById("CaptureOcx");
            //alert(CaptureOcx.GetPhotoFileArr());
            alert("扫描成功");
        } catch (e) {
            alert("err");
        }
    },
    ClearScanFileArr: function () {//清除扫描后的原文件
        try {
            var CaptureOcx = document.getElementById("CaptureOcx");
            CaptureOcx.ClearPhotoFileArr();
        } catch (e) {
            alert("err");
        }
    },
    FileUpload: function (url, tr) {//url:上传的远程网址
        var CaptureOcx = document.getElementById("CaptureOcx");
        //上传地址
        CaptureOcx.UploadServerUrl = url;
        var f = CaptureOcx.GetPhotoFileArr();
        var FileValue = "";
        if (f != "") {
            var fileArr = f.split(";");
            if (fileArr.length >= 1) {
                for (var i = 0; i < fileArr.length; i++) {
                    var filePath = "";
                    var r = CaptureOcx.SingleFileUpload(fileArr[i]);
                    if (r != "") {
                        var msg = r.split("|");
                        CaptureUploadInfo.Flag = msg[0];
                        CaptureUploadInfo.FilePath = msg[1];
                        CaptureUploadInfo.Msg = msg[2];
                        if (CaptureUploadInfo.Flag == "1") {
                            FileValue += CaptureUploadInfo.FilePath + "|";
                            //alert(CaptureUploadInfo.Msg);
                        } else {
                            alert("上传失败！\\n" + CaptureUploadInfo.Msg);
                        }
                    } else {
                        alert("上传失败！");
                    }
                }
            } else {
                alert("无文件！");
            }
            //CaptureOcx.ClearPhotoFileArr(); //清理服务器上保存的扫描文件记录
        } else {
            alert("无文件！");
        }
        Capture.HtmlPush(tr, FileValue);
        Capture.ClearScanFileArr();
    },
    Test: function () {
        try {
            var CaptureOcx = document.getElementById("CaptureOcx");
            alert(CaptureOcx.HelloWorld());
        } catch (e) {
            alert("err");
        }
    },
    HtmlPush: function (tr, values) {
        tr.closest('td').find("input[type='hidden']").val(values);
    }
};