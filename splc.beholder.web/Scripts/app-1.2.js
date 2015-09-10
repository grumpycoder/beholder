
//BEGIN READY DOM
$(function () {

    $.ajaxSetup({
        cache: false
    });

    $(document).ajaxSend(function () {
        $('#loading-pane').toggle(true);
    }).ajaxComplete(function () {
        $('#loading-pane').toggle(false);
    }).ajaxSuccess(function () {
        $('#loading-pane').toggle(false);
    }).ajaxStop(function () {
        $('#loading-pane').toggle(false);
    });

    initControls();

    $('.input-daterange').datepicker({
        todayBtn: "linked",
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true
    });

    $('.datePicker').datepicker({
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true

    });

    $('#combo_per').change(function () {
    });

    $("input:reset").on('click', function (e) {
        e.preventDefault();
        var url = window.location.href;

        $('.selectpicker').selectpicker('deselectAll');
        $('form input[type="text"]').val(null);
        $('form input[type="search"]').val(null);
        $(".chosen-select").val('').trigger("chosen:updated");
        if (url.indexOf('?') != -1) {
            $('#searchForm').submit();
        }

    });

    var config = {
        '.chosen-select': { width: '200px' },
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

});
//END DOM READY

function LoadView(target, action) {
    $.get(action, null, function (data) {
        $(target).html(data).effect('hightlight');
    });
}

function initControls() {
    var dropbox = $('#dropbox'),
        imageGallery = $('#imgGallery'),
        message = $('.message', dropbox),
        upload = $('#file'),
        mimeType = $('#Image_MimeType_Name'),
        imageFileName = $('#ImageFileName'),
        docmimeType = $('#MediaCorrespondenceContext_MimeType_Name'),
        docimageFileName = $('#MediaCorrespondenceContext_FileName'),
        pubmimeType = $('#MediaPublishedContext_MimeType_Name'),
        pubimageFileName = $('#MediaPublishedContext_FileName'),
        webmimeType = $('#MediaWebsiteEGroupContext_MimeType_Name'),
        webimageFileName = $('#MediaWebsiteEGroupContext_FileName'),
        pathArray = window.location.pathname.split('/'),
        protocol = window.location.protocol,
        uploadURL = protocol.concat('//', window.location.host, '/', pathArray[1], '/', 'UploadFiles'),
        fileIndex = 0;

    dropbox.filedrop({
        // The name of the $_FILES entry:
        paramname: 'file',
        maxfiles: 1,
        maxfilesize: 10, // in mb
        //url: string.concat(window.location.host,'/Home/UploadFiles'),
        url: uploadURL,
        allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif', 'image/tiff', 'image/bmp', 'application/pdf', 'image/x-png', 'image/pjpeg', 'image/jpeg', 'text/rtf', 'text/html', 'text/plain', 'application/vnd.ms-powerpoint',
            'application/msword', 'application/vnd.ms-excel', 'application/pdf', 'audio/wav', 'video/x-ms-wmv', 'image/tiff', 'application/zip', 'application/postscript', 'image/gif', 'message/rfc822', 'application/x-webarchive',
            'image/png', 'application/x-macbinary', 'image/jpeg', 'application/octet-stream', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
            'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'application/vnd.openxmlformats-officedocument.spreadsheetml.template', 'application/vnd.openxmlformats-officedocument.wordprocessingml.template'],

        data: {
            file: 'file',          // send POST variables
        },

        uploadFinished: function (i, file, response) {
            $.data(file).addClass('done');
            imageFileName.attr('value', file.name);
            mimeType.attr('value', file.type);
            docimageFileName.attr('value', file.name);
            docmimeType.attr('value', file.type);
            pubimageFileName.attr('value', file.name);
            pubmimeType.attr('value', file.type);
            webimageFileName.attr('value', file.name);
            webmimeType.attr('value', file.type);
            dropbox.hide();
        },
        error: function (err, file) {
            switch (err) {
                case 'BrowserNotSupported':
                    showMessage('Your browser does not support HTML5 file uploads!');
                    break;
                case 'TooManyFiles':
                    alert('Too many files! Please select only 1!');
                    break;
                case 'FileTooLarge':
                    alert(file.name + ' is too large! Please upload files up to 10mb.');
                    break;
                case 'FileTypeNotAllowed':
                    alert('FileType not allowed');
                    break;
                default:
                    break;
            }
        },
        // Called before each upload is started
        beforeEach: function (file) {
            if (fileIndex != 0) {
                alert('Only 1 file may be uploaded at a time');
                return false;
            }
        },
        uploadStarted: function (i, file, len) {
            fileIndex++;
            createImage(file);
        },
        progressUpdated: function (i, file, progress) {
            $.data(file).find('.progress').width(progress);
        },
    });

    var template =
        '<div class="preview">' +
            '<span class="imageHolder">' +
                '<img id="image" name="image" />' +
                '<span class="uploaded"></span>' +
            '</span>' +
            '<div class="progressHolder">' +
                '<div class="progress"></div>' +
            '</div>' +
        '</div>';

    function createImage(file) {
        var preview = $(template),
            image = $('img', preview),
            imageFileName = $('#ImageFileName'),
            mimeType = $('#Image_MimeType_Name'),
            docimageFileName = $('#MediaCorrespondenceContext_FileName'),
            docmimeType = $('#MediaCorrespondenceContext_MimeType_Name'),
            pubimageFileName = $('#MediaPublishedContext_FileName'),
            pubmimeType = $('#MediaPublishedContext_MimeType_Name');
        webimageFileName = $('#MediaWebsiteEGroupContext_FileName'),
        webmimeType = $('#MediaWebsiteEGroupContext_MimeType_Name');

        var reader = new window.FileReader();

        image.width = 100;
        image.height = 100;

        reader.onload = function (e) {
            image.attr('src', e.target.result);
        };

        // Reading the file as a DataURL. When finished,
        // this will trigger the onload function above:
        reader.readAsDataURL(file);

        //message.hide();
        preview.appendTo(imageGallery);
        // Associating a preview container
        // with the file, using jQuery's $.data():

        $.data(file, preview);
    }

    function showMessage(msg) {
        message.html(msg);
    }

    var $dlg = $('#dialog').dialog({
        autoOpen: false,
        width: 'auto',
        height: 'auto',
        position: 'center',
        beforeClose: function () {
            $(this).empty();
        }
    });

    $('.selectpicker').selectpicker();
    $('.datePicker').datepicker({
        todayBtn: "linked",
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true
    });


    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
            .addClass("custom-combobox")
            .insertAfter(this.element);
            this.element.hide();
            this._createAutocomplete();
        },
        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
                value = selected.val() ? selected.text() : "";

            this.input = $("<input>")
              .appendTo(this.wrapper)
              .val(value)
              .attr("title", this.element.attr("title"))
              .attr("id", this.element.attr("data-id"))
              .attr("data-url", this.element.attr("data-url"))
              .attr("data-hidden-field", this.element.attr("data-hidden-field"))
              .attr("data-val-required", this.element.attr("data-val-required"))
              .attr("required", this.element.attr("required"))
              .attr("data-val-is-passonnull", this.element.attr("data-val-is-passonnull"))
              .attr("data-val-is-operator", this.element.attr("data-val-is-operator"))
              .attr("data-val-is-dependentproperty", this.element.attr("data-val-is-dependentproperty"))
              .attr("data-val-is", this.element.attr("data-val-is"))
              .attr("data-val", this.element.attr("data-val"))
              .attr("data-val-date", this.element.attr("data-val-date"))
              .addClass("form-control")
              .autocomplete({
                  delay: 500,
                  minLength: 3,
                  source: function (request, response) {
                      var url = this.element.attr("data-url");
                      $.ajax({
                          url: url,
                          dataType: "json",
                          data: {
                              term: request.term
                          },
                          success: function (data) {
                              if (data.length == 0) {
                                  $(this).val("");
                                  alert("No matches found");
                              }
                              response($.map(data, function (item) {
                                  return { label: item.label, value: item.Id };
                              }));
                          }
                      });
                  },
                  select: function (event, ui) {
                      event.preventDefault();
                      var hidden = $(this).attr("data-hidden-field");

                      $(this).val(ui.item.label);
                      $('#' + hidden).val(ui.item.value);
                  }
              })
              .tooltip({
                  tooltipClass: "ui-state-highlight"
              });
        },

        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;

            $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "Show All Items")
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("custom-combobox-toggle ui-corner-right")
              .mousedown(function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .click(function () {
                  input.focus();

                  // Close if already visible
                  if (wasOpen) {
                      return;
                  }

                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            // Remove invalid value
            this.input
              .val("")
              .attr("title", value + " didn't match any item")
              .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.data("ui-autocomplete").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });

    $('body').on('carousel', '#myCarousel', function () {
        interval: 5000;
    });

    $('body').on('click', '.btnDetails', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        $dlg
            .dialog({
                title: 'Details',
                buttons: {
                    "Close": function () {
                        $(this).dialog('close');
                    }
                }
            })
            .load(url).dialog('open');
    });

    $('body').on('click', '.btnNew', function (e) {
        e.preventDefault();
        var url = this.href;
        var $target;
        var $refresh = $(this).data('refresh');
        var $action = $(this).data('action');
        if ($(this).data('target') == null && $refresh) {
            $target = $(this).parents("div:first");
        }
        else {
            $target = $(this).data('target');
            $refresh = true;
        }
        $('#dialog')
        .dialog({
            title: $(this).data('title'),
            autoOpen: false,
            resizable: true,
            modal: true,
            position: { my: "center", at: "top+20%", collision: 'fit', of: window },
            width: '500',
            height: 'auto',
            buttons: {
                "Save": function () {
                    //TODO: should not have to specify dialog name
                    var $form = $('#dialog form');
                    $.validator.unobtrusive.parse($form);
                    if ($form.valid()) {
                        $.post(url, $form.serialize(),
                            function () {
                                toastr.success('new record created');
                                if ($refresh)
                                    LoadView($target, $action);
                            });
                        $(this).dialog('close');
                    }
                },
                "Cancel": function () { $(this).dialog('close'); }
            }
        })
        .load(url).dialog('open');

        $('#dialog').dialog('open');

    });

    $('body').on('click', '.btnEdit', function (e) {
        e.preventDefault();
        var url = this.href;
        var $target;
        var $refresh = $(this).data('refresh');
        var $action = $(this).data('action');
        if ($(this).data('target') == null && $refresh) {
            $target = $(this).parents("div:first");
        }
        else {
            $target = $(this).data('target');
        }
        $dlg
            .dialog({
                title: $(this).data('title'),
                autoOpen: false,
                resizable: true,
                modal: true,
                position: { my: "center", at: "top+20%", collision: 'fit', of: window },
                width: '500',
                height: 'auto',
                buttons: {
                    "Save": function () {
                        //TODO: should not have to specify dialog name
                        var $form = $('#dialog form');
                        $.validator.unobtrusive.parse($form);
                        if ($form.valid()) {
                            $.post(url, $form.serialize(),
                                function () {
                                    toastr.success('record updated');
                                    if ($refresh)
                                        LoadView($target, $action);
                                });
                            $(this).dialog('close');
                        }
                    },
                    "Cancel": function () { $(this).dialog('close'); }
                }
            }).load(url).dialog('open');
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var $target;
        var $refresh = $(this).data('refresh');
        var url = $(this).attr('href');
        var $action = $(this).data('action');
        var $message = $(this).data('message');
        if ($message === undefined) {
            $message = 'Are you sure you want to delete';
        };
        var success = $(this).data('success');
        if (success === undefined) {
            success = 'record deleted';
        }

        var $dlg = $('#dialog').dialog();
        if ($(this).data('target') == null && $refresh) {
            $target = $(this).parents("div:first");
        }
        else {
            $target = $(this).data('target');
        }
        $dlg
            .dialog({
                title: $(this).data('title'),
                buttons: {
                    "Ok": function () {
                        $(this).dialog('close');
                        $.ajax({
                            url: url,
                            type: "POST",
                            success: function () {
                                toastr.success(success);
                                if ($refresh)
                                    LoadView($target, $action);
                            },
                            error: function (data) {
                                toastr.error(data);
                            }
                        });
                    },
                    "Cancel": function () { $(this).dialog('close'); }
                }
            })
            .html($message)
            .dialog('open');
    });

    $('body').on('click', '.btnApprove', function (e) {
        e.preventDefault();
        var $target;
        var $refresh = $(this).data('refresh');
        var url = $(this).attr('href');
        var $action = $(this).data('action');

        if ($(this).data('target') == null && $refresh) {
            $target = $(this).parents("div:first");
        }
        else {
            $target = $(this).data('target');
        }

        $.ajax({
            url: url,
            type: "POST",
            success: function () {
                toastr.success('record updated');
                if ($refresh)
                    LoadView($target, $action);
            },
            error: function (data) {
                toastr.error(data);
            }
        });

    });

    $('input[type=radio]').click(function () {
        if ($(this).val() == 'Yes') {
            $("#uploadFile").slideDown("fast"); //Slide Down Effect
            $("#createFile").slideUp("fast"); //Slide Up Effect
        } else {
            $("#uploadFile").slideUp("fast");  //Slide Up Effect
            $("#createFile").slideDown("fast"); //Slide Down Effect
        }

    });

}

(function ($) {

    $.validator.unobtrusive.adapters.addSingleVal("futuredate", "currentdate");

    $.validator.addMethod("futuredate",
        function (inputValue, inputElement, selectedate) {
            var returnValue = true;
            if (inputValue) {
                var current = new Date();
                if (Date.parse(inputValue) > current) {
                    returnValue = false;
                }
            }
            return returnValue;
        });
}(jQuery));

function initPartialControls() {
    var $form = $('form');
    $.validator.unobtrusive.parse($form);

    $('.datePicker').datepicker({
        todayBtn: "linked",
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true
    });

    $("#combobox_per").combobox();
    $("#combobox_org").combobox();
    $("#combobox_events").combobox();
    $("#combobox_mediaImages").combobox();
    $("#combobox_vehicle").combobox();
    $("#combobox_mav").combobox();
    $("#combobox_corr").combobox();
    $("#combobox_pub").combobox();
    $("#combobox_sub").combobox();
    $("#combobox_subscription").combobox();

}