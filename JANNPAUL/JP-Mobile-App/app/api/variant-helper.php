
<script>
    function changeTryItOnPicture(variant_id) {
        uploader = "#try_it_on_upload_" + variant_id;
        $(uploader).click();
        
        // Input type file controlling
        $(uploader).unbind("change").bind("change", function () {
            if (this.files && this.files[0]) {
                var file = this.files[0];
                var formData = new FormData();
                formData.append('picture', file);
                formData.append('variant_id', variant_id);
                
                //Show icon
                $('#try_it_on_pic_' + variant_id).attr("src","./dist/img/loading.gif");
                // Server level
                $.ajax({
                    url: '/app/api/save-try-it-on-picture.php',
                    type: 'POST',
                    data: formData,
                    //Options to tell JQuery not to process data or worry about content-type
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //Client
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#try_it_on_pic_' + variant_id).attr('src', e.target.result);
                        }
                        reader.readAsDataURL(file);
                    }
                });
            }
        });
    }
</script>
