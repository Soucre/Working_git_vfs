<!-- @@TITLE=(New Product)-->
<!-- @@DESCRIPTION=Please fill in product's information-->
<!-- @@BREADCRUMB=<a href="?products">Products</a>#Product-->
<?php
$product_id = 0;
$db = database\getMySqli();

if (filter_has_var(INPUT_POST, "submit")) {
    //Save mode
    $product_id = filter_input(INPUT_POST, "form-product-id");
    // Save mode
    $title = $db->real_escape_string(filter_input(INPUT_POST, "title"));
    $category = $db->real_escape_string(filter_input(INPUT_POST, "category"));
    $description = $db->real_escape_string(filter_input(INPUT_POST, "description"));
    $visible = $db->real_escape_string(filter_input(INPUT_POST, "visible"));
    $metal = $db->real_escape_string(filter_input(INPUT_POST, "metal"));
    $sharing_url = $db->real_escape_string(filter_input(INPUT_POST, "sharing_url"));

    if ($visible !== '1') {
        $visible = "0";
    }

    //Picture uploading
    $uploadOk = false;
    if ($_FILES["picture"]["name"] && $_FILES["picture"]["tmp_name"]) {
        $target_dir = "dist/img/product/";

        $file_name = basename($_FILES["picture"]["name"]);
        $target_file = $target_dir . $file_name;
        $imageFileType = pathinfo($target_file, PATHINFO_EXTENSION);
        $check = getimagesize($_FILES["picture"]["tmp_name"]);
        $file_msg = "";
        if ($check) {
            $uploadOk = true;
            // Check file size
            if ($_FILES["picture"]["size"] > 3145728) {
                $file_msg .= "<br>Picture file size is too large.";
                $uploadOk = false;
            }
            // Allow certain file formats
            if ($imageFileType != "jpg" && $imageFileType != "png" && $imageFileType != "jpeg" && $imageFileType != "gif") {
                $file_msg .= "<br>Sorry, only JPG, JPEG, PNG & GIF files are allowed.";
                $uploadOk = false;
            }
            // Check if $uploadOk is set to 0 by an error
            if (!$uploadOk) {
                $file_msg .= "<br>(Your picture was not uploaded).";
                // if everything is ok, try to upload file
            } else {
                if (move_uploaded_file($_FILES["picture"]["tmp_name"], $target_file)) {
                    //OK, no need message      
                    // call the function that will create the thumbnail. The function will get as parameters 
                    //the image name, the thumbnail name and the width and height desired for the thumbnail
                    //$thumb = \image_utils\make_thumb($target_file, $target_file, 500, 500);
                } else {
                    $file_msg .= "<br>Sorry, there was an error uploading your file.";
                    $uploadOk = false;
                }
            }
        } else {
            $file_msg .= "<br>File is not an image.";
        }
    }
    $set_statement = "SET `title` = '$title', `description`='$description', `visible` = $visible, "
            . "`metal` = '$metal', `sharing_url` = '$sharing_url', "
            . "`category` = '$category'";
    if ($uploadOk && file_exists($target_file)) {
        $set_statement .= ", picture = '$target_file'";
    } else {
        $uploadOk = false;
    }

    if ($product_id == 0) {
        // Create new
        $sql = "INSERT INTO `product` $set_statement";
    } else {
        // Save current
        $sql = "UPDATE `product` $set_statement WHERE `product_id` = $product_id";
    }
    //echo $sql;
    $db->query($sql);
    if ($db->affected_rows >= 1) {
        if ($product_id == 0) {
            $product_id = $db->insert_id;
        }
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($title) . " has been saved." . $password_msg;
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "success";
    } else {
        $_SESSION[APPLICATION_MESSAGE] = htmlspecialchars($title) . " has not been saved." . $file_msg . "<br>" . $db->error;
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "warning";
    }
    $_SESSION[APPLICATION_MESSAGE] .= " What do you want to do next?<br>"
            . "Continue editing, <a href=\"./?product\">create new product</a>, or go back to <a href=\"./?products\">Products</a>.";
} else {
    $product_id = filter_input(INPUT_GET, "product");
}

$found = false;
if ($product_id > 0) {
    $result = $db->query("SELECT * FROM `product` WHERE `product_id` = $product_id");
    if ($result) {
        $product = $result->fetch_object();
        $found = true;
    }
    if (!$found) {
        $_SESSION[APPLICATION_MESSAGE] = "Product #$product_id not found. You may not have the right to view, or it has been deleted.";
        $_SESSION[APPLICATION_MESSAGE_TYPE] = "danger";
    }
    $result->close();
    echo '<script>contentHeaderTitle_SetContent("' . htmlspecialchars($product->title) . '<small>Product details</small>")'
    . '</script>';
}

include './app/form-notification.php';
include "./app/api/variant-helper.php";
?>

<!-- Select2 -->
<link href="plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
<style>
    td.details-control {
        background: url('https://www.datatables.net/examples/resources/details_open.png') no-repeat center center;
        cursor: pointer;
    }
    tr.shown td.details-control {
        background: url('https://www.datatables.net/examples/resources/details_close.png') no-repeat center center;
    }
</style>
<img src="./dist/img/loading.gif" id="loading-indicator" style="display:none" />
<style>
    #loading-indicator {
        width: 45px;
        height: 45px;
        position: fixed;
        top: 50%;
        left: 50%;
        /* bring your own prefixes */
        transform: translate(-50%, -50%);
        z-index: 1000;
    }
</style>
<form role="form" action="./?product=<?php echo $product_id; ?>" method="POST" enctype="multipart/form-data">
    <input type="hidden" value="<?php echo $product_id; ?>" id="form-product-id" name="form-product-id" />
    <div class="row">
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Overview</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                <div class="box-body">
                    <div class="form-group">
                        <label for="title">Title *</label>
                        <input required="true" class="form-control" id="title" value="<?php echo htmlspecialchars($product->title); ?>" name="title" placeholder="Enter product's name" type="text">
                    </div>
                    <div class="form-group">
                        <label>Category</label>
                        <select class="form-control select2" id="category" name="category">
                            <option value="">(Unspecified)</option>
                            <?php
                            $result = $db->query("SELECT `category_id`, `title` from `category` ORDER BY `title`");
                            if ($result) {
                                while ($category = $result->fetch_object()) {
                                    echo '<option value="' . $category->category_id . '"';
                                    if ($product->category === $category->category_id) {
                                        echo ' selected';
                                    }
                                    echo '>' . $category->title . '</option>';
                                }
                                $result->close();
                            }
                            ?>
                        </select>
                    </div><!-- /.form-group -->
                    <div class="form-group">
                        <label>Metal</label>
                        <select class="form-control select2" id="metal" name="metal">
                            <option value="">(Unspecified)</option>
                            <option value="White Gold" <?php
                            if ($product->metal === 'White Gold') {
                                echo ' selected';
                            }
                            ?>>White Gold</option>
                            <option value="Yellow Gold" <?php
                            if ($product->metal === 'Yellow Gold') {
                                echo ' selected';
                            }
                            ?>>Yellow Gold</option>
                            <option value="Rose Gold" <?php
                            if ($product->metal === 'Rose Gold') {
                                echo ' selected';
                            }
                            ?>>Rose Gold</option>
                            <option value="Platinum" <?php
                            if ($product->metal === 'Platinum') {
                                echo ' selected';
                            }
                            ?>>Platinum</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label>Description</label>
                        <textarea id="description" name="description" class="form-control" rows="3" placeholder="Enter description"><?php echo htmlspecialchars($product->description); ?></textarea>
                    </div>    
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="visible" name="visible" value="1" <?php echo $product->visible ? 'checked' : '' ?>> Publish
                        </label>
                    </div>
                </div><!-- /.box-body -->

            </div><!-- /.box -->         
        </div>  

        <div class="col-md-6">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Picture</h3>
                </div><!-- /.box-header -->
                <div class="box-body">
                    <div class="form-group">
                        <label for="picture">Cover Image (Default)</label>
                        <p align="center" class="help-block"><?php
                            if ($product->picture) {
                                echo "<img style=\"width: 100%;max-width: 134px;height: auto;\" src=\"$product->picture\" alt=\"Product Picture\" />";
                            } else {
                                echo 'Please pick a product picture.';
                            }
                            ?></p>
                        <input id="picture" name="picture" type="file" accept="image/gif, image/jpeg, image/png" >

                    </div>    

                </div>
            </div>

            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Social</h3>
                </div><!-- /.box-header -->
                <div class="box-body">
                    <div class="form-group">
                        <label for="sharing_url">Custom Sharing URL</label>
                        <input class="form-control" id="sharing_url" 
                               value="<?php echo htmlspecialchars($product->sharing_url); ?>" name="sharing_url" placeholder="Enter custom url to share for this product" type="url">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <?php if ($product_id > 0) { ?>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-default">
                    <div class="box-header">
                        <h3 class="box-title">Product Variants</h3>
                    </div><!-- /.box-header -->
                    <div class="box-body">
                        <table id="variants" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Title</th>
                                    <th>Carat</th>
                                    <th>Diamond Cut</th>
                                    <th>Setting</th>
                                    <th>Try It On Picture</th>
                                    <th></th>
                                </tr>
                            </thead>
                        </table>
                    </div><!-- /.box-body -->
                    <div class="box-footer">
                        <div class="form-group" style="display: none;" id="add_variant_div">
                             <label>Variant's title</label>
                            <input type="text" class="form-control" id="variant_title" name="variant_title" placeholder="Enter description for this variant">
                            <label>Carat *</label>
                            <input type="text" class="form-control" id="addNewVariant" name="addNewVariant" placeholder="Enter new Carat value to add...">
                            <label>Diamond Cut *</label>
                            <select class="form-control select2" id="diamond_cut" name="diamond_cut">
                                <option value="Super Ideal">Super Ideal</option>
                                <option value="Solasfera Round">Solasfera Round</option>
                                <option value="Cushion Brellia">Cushion Brellia</option>
                                <option value="Octagon D'Amor">Octagon D'Amor</option>
                            </select>
                            <label>Setting *</label>
                            <select class="form-control select2" id="setting" name="setting">
                                <option value="Solitaire">Solitaire</option>
                                <option value="Diamond Settings">Diamond Settings</option>
                                <option value="Sidestones">Sidestones</option>
                                <option value="Halo">Halo</option>
                                <option value="Verragio">Verragio</option>
                            </select>
                        </div>
                        <input type="button" id="variantSubmitButton" onclick="showVariantForm();" class="btn btn-success btn-sm" value="Add new variant" />
                        <input type="button"  onclick="cancelVariantForm();" style="padding-left: 5px;" class="btn btn-default btn-sm" value="Cancel" />
                    </div>
                </div><!-- /.box -->
            </div>
        </div>
    <?php } ?>
    <!-- Buttons -->
    <div class="row no-print">
        <div class="col-xs-12">
            <a href="#" target="_blank" class="btn btn-default disabled"><i class="fa fa-print"></i> Print</a>
            <button type="submit" id="submit" name="submit" class="btn btn-success pull-right "><i class="fa fa-save"></i> Save Product</button>
            <a href="./?products" class="btn btn-default pull-right" style="margin-right: 5px;"><i class="fa fa-close"></i> Discard Changes</a>
        </div>
    </div>
</form>

<!-- DATA TABLES -->
<link href="./plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
<!-- DATA TABES SCRIPT -->
<script src="./plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="./plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
<!-- Select2 -->
<script src="./plugins/select2/select2.full.min.js" type="text/javascript"></script>
<!-- page script -->
<script type="text/javascript">
                            var _currentVariantRowToExpand = null;
                            var _currentVariantTrToExpand = null;
                            $(document).ready(function () {

                                $('#variants').DataTable({
                                    processing: true,
                                    serverSide: true,
                                    ajax: "../app/api/variants.php?product_id=<?php echo $product_id; ?>",
                                    "searching": false,
                                    "stateSave": true,
                                    "paging": false,
                                    "ordering": false,
                                    "info": false,
                                    columns: [
                                        {
                                            "className": 'details-control',
                                            "orderable": false,
                                            "data": null,
                                            "defaultContent": ''
                                        }
                                        ,
                                        {
                                            name: 'title'
                                        },
                                        {
                                            name: 'carat'
                                        }
                                        ,
                                        {
                                            name: 'diamond_cut'
                                        }
                                        ,
                                        {
                                            name: 'setting'
                                        }
                                        ,
                                        {name: 'try_it_on_picture', orderable: false, searchble: false}
                                        ,
                                        {name: 'command', orderable: false, searchble: false}
                                    ]
                                });
                                $('#similar_products').DataTable({
                                    processing: true,
                                    serverSide: true,
                                    ajax: "../app/api/similar-products.php?product_id=<?php echo $product_id; ?>",
                                    "searching": false,
                                    "stateSave": true,
                                    "paging": false,
                                    "ordering": false,
                                    "info": false,
                                    columns: [
                                        {name: 'picture', orderable: false, searchble: false},
                                        {name: 'title'},
                                        {name: 'command', orderable: false, searchble: false}
                                    ]
                                });

                                // Try it on child pictures
                                // Add event listener for opening and closing details
                                $('#variants tbody').on('click', 'td.details-control', function () {
                                    var tr = $(this).closest('tr');
                                    var row = $('#variants').DataTable().row(tr);

                                    if (row.child.isShown()) {
                                        // This row is already open - close it
                                        row.child.hide();
                                        tr.removeClass('shown');
                                    }
                                    else {
                                        // Open this row
                                        row.child(format(row.data())).show();
                                        tr.addClass('shown');
                                        _currentVariantRowToExpand = row;
                                        _currentVariantTrToExpand = tr;
                                    }
                                });
                            });

                            /* Formatting function for row details - modify as you need */
                            function format(d) {
                                // `d` is the original data object for the row
                                return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                                        d[0] +
                                        '</table>';
                            }

                            // Add new variant
                            function addNewVariantRow() {
                                var carat = $("#addNewVariant").val();
                                if (carat) {
                                    $("#addNewVariant").val("");
                                    var variant_title = encodeURIComponent($('#variant_title').val());
                                    $.ajax({
                                        url: '/app/api/add-variant.php?product_id=<?php echo $product_id; ?>&carat=' + carat +
                                                "&diamond_cut=" + $("#diamond_cut").val() + "&setting=" + $("#setting").val() + "&variant_title=" + 
                                                variant_title,
                                        type: 'GET',
                                        //Options to tell JQuery not to process data or worry about content-type
                                        cache: false,
                                        contentType: false,
                                        processData: false,
                                        success: function (data) {
                                            //Client
                                            $('#variants').DataTable().ajax.reload();
                                        }
                                    });
                                }
                            }

                            function deleteVariant(variant_id) {
                                $.ajax({
                                    url: '/app/api/delete-variant.php?variant_id=' + variant_id,
                                    type: 'GET',
                                    //Options to tell JQuery not to process data or worry about content-type
                                    cache: false,
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        //Client
                                        $('#variants').DataTable().ajax.reload();
                                    }
                                });
                            }

                            function deleteSimilarProduct(product_id_1, product_id_2) {
                                $.ajax({
                                    url: '/app/api/delete-similar-product.php?product_id_1=' + product_id_1 + "&product_id_2=" + product_id_2,
                                    type: 'GET',
                                    //Options to tell JQuery not to process data or worry about content-type
                                    cache: false,
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        //Client
                                        $('#similar_products').DataTable().ajax.reload();
                                    }
                                });
                            }

                            var variantFormShown = false;

                            function showVariantForm() {
                                if (!variantFormShown) {
                                    $("#add_variant_div").slideDown('slow', function () {
                                        variantFormShown = true;
                                        $("#variantSubmitButton").val("Save");
                                    });
                                    //
                                } else {
                                    addNewVariantRow();
                                    cancelVariantForm();
                                }
                            }

                            function cancelVariantForm() {
                                $("#add_variant_div").slideUp('fast', function () {
                                    variantFormShown = false;
                                    $("#variantSubmitButton").val("Add new variant");
                                });
                            }

                            function addVariantPicture(variant_id) {
                                uploader = "#add_variant_picture_" + variant_id;
                                $(uploader).click();

                                // Input type file controlling
                                $(uploader).unbind("change").bind("change", function () {
                                    if (this.files && this.files[0]) {
                                        var file = this.files[0];
                                        var formData = new FormData();
                                        formData.append('picture', file);
                                        formData.append('variant_id', variant_id);

                                        //Show icon
                                        //$('#try_it_on_pic_' + variant_id).attr("src", "./dist/img/loading.gif");
                                        $('#loading-indicator').show();
                                        // Server level
                                        $.ajax({
                                            url: '/app/api/save-variant-picture.php',
                                            type: 'POST',
                                            data: formData,
                                            //Options to tell JQuery not to process data or worry about content-type
                                            cache: false,
                                            contentType: false,
                                            processData: false,
                                            success: function (data) {
                                                //Client
                                                $('#variants').DataTable().ajax.reload(function (json) {
                                                    if (_currentVariantRowToExpand !== null &&
                                                            _currentVariantTrToExpand !== null) {
                                                        _currentVariantRowToExpand.child(format(_currentVariantRowToExpand.data())).show();
                                                        _currentVariantTrToExpand.addClass('shown');
                                                    }
                                                });
                                                $('#loading-indicator').hide();
                                            }
                                        });
                                    }
                                });
                            }
</script>