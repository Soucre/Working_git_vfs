
$(document).ready(function () {
    $('button[id=ms-dropReportType]').click(function () {
        if (!$('#ms-SelectReportType').is(':visible')) {
            $('#ms-SelectReportType').fadeIn();
            $('#Arr-open').addClass('open');
        }
        else {
            $('#ms-SelectReportType').fadeOut();
            $('#Arr-open').removeClass('open');
        }
    });


    // click on readmore xem thêm
    $('a[id=readMore]').click(function () {
        $(this).attr('disabled', true);
        var rowCurrent = $('.ctn-view-list').find('.item-research').length; // so dong hien tai de lay them 5
        var ticker = $('#hiddenTicker').val();
        var dropboxFilter = [];
        $("input[name='CategoryIDs']:checked").each(function () { // kiem tra xem bao nhieu check box filter
            dropboxFilter.push(parseInt($(this).val()));
        });

        LoadMore(rowCurrent, ticker, dropboxFilter);
        return false;
    });
    function LoadMore(skipPostion, ticker, dropboxFilter) {

        $.ajax({
            url: '/Research/LoadMoreReport',
            type: "GET",
            traditional: true,
            async: false,
            data: { "skipPostion": skipPostion, "ticker": ticker, "CategoryIDs": dropboxFilter },
            beforeSend: function (xhr) {
                $("#loading-image").show();
            },
            success: function (data) {
                //$('.ajaxLoadingImage').html('');                    
                $(data).appendTo(".ctn-view-list").hide().fadeIn(500);
                //$(".ctn-view-list").load(data);
                $('#readMore').removeAttr('disabled', false);
                $("#loading-image").hide();
            }
        });
    }

});
    
