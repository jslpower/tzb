<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoMsgAdd.aspx.cs" Inherits="Enow.TZB.Web.Manage.WX.AutoMsgAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>新增自劝回复消息</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/TableUtil.js" type="text/javascript"></script>
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
    <div class="firsttable">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th width="150" height="40" align="right">
                    消息类型：
                </th>
                <td><input type="radio" id="rblType" name="rblType" value="0" checked="checked" />文本回复<input type="radio" id="rblType" name="rblType" value="1" />图文回复</td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    留言内容：
                </th>
                <td>
                    <asp:TextBox ID="txtQuestion" CssClass="input-txt formsize180" runat="server" valid="required"
                                errmsg="请填写留言内容！"></asp:TextBox><span
                        class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    回复内容：
                </th>
                <td><div id="divText">
                    <asp:TextBox ID="txtAnswer" CssClass="input-txt formsize180" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox><span
                        class="fontred">*</span></div><script type="text/javascript">
                                                          var Journey = {
                                                              //删除附件
                                                              RemoveFile: function (obj) {
                                                                  $(obj).closest("td").find("input[name='hide_Journey_file']").val("");
                                                                  $(obj).closest("div[class='upload_filename']").remove();
                                                                  return false;
                                                              },
                                                              CreateFlashUpload: function (flashUpload, idNo) {
                                                                  flashUpload = new SWFUpload({
                                                                      upload_url: "/CommonPage/upload.aspx",
                                                                      file_post_name: "Filedata",
                                                                      post_params: {
                                                                          "ASPSESSID": "<%=Session.SessionID %>"
                                                                      },

                                                                      file_size_limit: "2 MB",
                                                                      file_types: "*.bmp;*.jpg;*.gif;*.jpeg;*.png;",
                                                                      file_types_description: "附件上传",
                                                                      file_upload_limit: 100,
                                                                      swfupload_loaded_handler: function () { },
                                                                      file_dialog_start_handler: uploadStart,
                                                                      upload_start_handler: uploadStart,
                                                                      file_queued_handler: fileQueued,
                                                                      file_queue_error_handler: fileQueueError,
                                                                      file_dialog_complete_handler: fileDialogComplete,
                                                                      upload_progress_handler: uploadProgress,
                                                                      upload_error_handler: uploadError,
                                                                      upload_success_handler: uploadSuccess,
                                                                      upload_complete_handler: uploadComplete,

                                                                      // Button settings
                                                                      button_placeholder_id: "spanButtonPlaceholder_" + idNo,
                                                                      button_image_url: "/images/swfupload/XPButtonNoText_178_34.gif",
                                                                      button_width: 178,
                                                                      button_height: 34,
                                                                      button_text: '<span ></span>',
                                                                      button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                                                                      button_text_top_padding: 1,
                                                                      button_text_left_padding: 5,
                                                                      button_cursor: SWFUpload.CURSOR.HAND,
                                                                      flash_url: "/js/swfupload/swfupload.swf",
                                                                      custom_settings: {
                                                                          upload_target: "divFileProgressContainer_" + idNo,
                                                                          HidFileNameId: "hide_Journey_file_" + idNo,
                                                                          HidFileName: "hide_Journey_file_Old",
                                                                          ErrMsgId: "errMsg_" + idNo,
                                                                          UploadSucessCallback: function () { Journey.UploadOverCallBack(idNo); }
                                                                      },
                                                                      debug: false,
                                                                      minimum_flash_version: "9.0.28",
                                                                      swfupload_pre_load_handler: swfUploadPreLoad,
                                                                      swfupload_load_failed_handler: swfUploadLoadFailed
                                                                  });
                                                              },
                                                              UploadArgsList: [],
                                                              InitSwfUpload: function (tr, no) {
                                                                  var $box = tr || $("#tbl_Cer");
                                                                  $box.find("div[data-class='Journey_upload_swfbox']").each(function () {
                                                                      var idNo = no || parseInt(Math.random() * 100000);

                                                                      $(this).find("[data-class='Journey_upload']").each(function () {
                                                                          //alert($(this).attr("id"));
                                                                          //if ($(this).attr("id") == "") {
                                                                              $(this).attr("id", $(this).attr("data-id") + "_" + idNo);
                                                                          //}
                                                                      })
                                                                      var swf = null;
                                                                      Journey.CreateFlashUpload(swf, idNo);
                                                                      if (swf) {
                                                                          Journey.UploadArgsList.push(swf);
                                                                      }
                                                                  })
                                                              },
                                                              AddRowCallBack: function (tr) {
                                                                  var $tr = tr;
                                                                  $tr.find("div[data-class='Journey_upload_swfbox']").html($("#divJourneyUploadTemp").html());
                                                                  $tr.find("div[data-class='span_journey_file']").remove();
                                                                  Journey.InitSwfUpload($tr);
                                                              }
                                                          }
    $(function () {
        Journey.InitSwfUpload(null, null);
    })
</script>
                    <div id="divNews" style="display:none">
					<table width="100%" cellspacing="0" cellpadding="0" border="0" id="tbl_Cer" class="table_C autoAdd">
                <tbody>
                    <tr><th>标题</th><th>描述</th><th>图片</th><th>链接网址</th><th>操作</th></tr>
                    <tr class="tempRow"><td><input type="text" name="txtTitle" id="txtTitle" maxlength="200" /></td><td><input type="text" name="txtDescription" id="txtDescription" maxlength="200" /></td><td>
<div style="margin: 5px 5px;" data-class="Journey_upload_swfbox">
                        <div>
                            <input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file" />
                            <span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span><span data-class="Journey_upload"
                                data-id="errMsg" class="errmsg"></span>
                        </div>
                        <div data-class="Journey_upload" data-id="divFileProgressContainer">
                        </div>
                        <div data-class="Journey_upload" data-id="thumbnails">
                        </div>
                    </div>
</td><td><input type="text" name="txtUrl" id="txtUrl" maxlength="200" /></td><td><div class="caozuo">
                                <ul>
                                    <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                                    <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span>
                                        删除</span></a></li>
                                </ul>
                            </div></td></tr></tbody></table></div>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" id="btnSave">保 存 &gt;&gt;</a></li>
                <li><a href="javascript:void(0);" id="btnCanel">关 闭 &gt;&gt;</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </div>
    </form>
   <div style="margin:10px 2px; display: none;" id="divJourneyUploadTemp">
        <div>
            <input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file" />
            <span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span><span data-class="Journey_upload"
                data-id="errMsg" class="errmsg"></span>
        </div>
        <div data-class="Journey_upload" data-id="divFileProgressContainer">
        </div>
        <div data-class="Journey_upload" data-id="thumbnails">
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Data: {
                MId: '<%=Request.QueryString["MId"] %>',
                SMId: '<%=Request.QueryString["SMId"] %>',
                act: '<%=Request.QueryString["act"] %>',
                CId: '<%=Request.QueryString["CId"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            HideForm: function () {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '',
                    title: "",
                    width: "710px",
                    height: "420px"
                }
            },
            ShowBoxy: function (data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            CheckForm: function () {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");

            },
            Form: null,
            Save: function () {
                $("#btnSave").text("提交中...");
                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "AutoMsgAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                PageJsDataObj.HideForm();
                                parent.window.location.href = parent.window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            $("#btnSave").text("保 存");
                            PageJsDataObj.BindBtn();
                        }
                    },
                    error: function () {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        $("#btnSave").text("保 存");
                        PageJsDataObj.BindBtn();
                    }
                });
            },
            PageInit: function () {
                $("#tbl_Cer").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtnFinaPlan", delButtonClass: "delbtnFinaPlan", max: 9, addCallBack: Journey.AddRowCallBack });
            },
            BindBtn: function () {
                $('input:radio[name=rblType]').change(function () {
                    var rbV = $(this).val();
                    if (rbV == "0") {
                        $("#divText").show();
                        $("#divNews").hide();
                    } else {
                        $("#divNews").show();
                        $("#divText").hide();
                    }
                });
                $("#btnSave").unbind("click").click(function () {
                    if (PageJsDataObj.CheckForm()) {
                        PageJsDataObj.Save();
                    }
                });
                $("#btnCanel").unbind("click").click(function () {
                    PageJsDataObj.HideForm();
                    return false;
                })
            }
        }
        $(function () {            
            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
</script>
</body>
</html>
