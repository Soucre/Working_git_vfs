<?php

require_once dirname(__DIR__) . '/api/api-header.php';

//Ajax calling does not pass the session data to
 if(!isset($_SESSION[MEMBER_ID])) {
  return;
  }

if (!filter_has_var(INPUT_POST, "variant_id")) {
    return;
}

$db = \database\getMySqli();
 $db->query("delete from `debug`");
 
$file_msg = "";
//Picture uploading
$uploadOk = false;
if ($_FILES["picture"]["name"] && $_FILES["picture"]["tmp_name"]) {
    $target_dir = "dist/img/product/";

    $file_name = basename($_FILES["picture"]["name"]);
    $target_file = $target_dir . uniqid() . '-' . $file_name;
    $imageFileType = pathinfo($target_file, PATHINFO_EXTENSION);
    $check = getimagesize($_FILES["picture"]["tmp_name"]);

    $db->query("insert into `debug`(`text`) values('" . $file_name . "')");
    $db->query("insert into `debug`(`text`) values('Target file: " . $target_file . "')");
    $db->query("insert into `debug`(`text`) values('Variant ID: " . filter_input(INPUT_POST, "variant_id") . "')");

    if ($check) {
        $uploadOk = true;
        // Check file size
        if ($_FILES["picture"]["size"]> 3145728) {
            $file_msg .= "<br>Picture file size is too large.";
            $uploadOk = false;
        }
        // Allow certain file formats
        if ($imageFileType != "png" && $imageFileType != "gif" && $imageFileType != "jpeg" && $imageFileType != "jpg") {
            $file_msg .= "Sorry, only PNG, gif or jpeg / jpg is allowed.";
            $uploadOk = false;
        }
        // Check if $uploadOk is set to 0 by an error
        if (!$uploadOk) {
            $file_msg .= "<br>(Your picture was not uploaded).";
            // if everything is ok, try to upload file
        } else {
            if (move_uploaded_file($_FILES["picture"]["tmp_name"], "../../" . $target_file)) {
                
            } else {
                $file_msg .= "Sorry, there was an error uploading your file.";
                $uploadOk = false;
            }
        }
    } else {
        $file_msg .= "<br>File is not an image.";
    }
}
if ($uploadOk && file_exists("../../" . $target_file)) {
    // Save current
    $sql = "INSERT INTO `product_variant_picture`(`variant_id`, `picture`) VALUES(" . filter_input(INPUT_POST, "variant_id") . 
            ", '$target_file')" ;

//echo $sql;
    $db->query($sql);
    if ($db->affected_rows >= 1) {
        $db->query("insert into `debug`(`text`) values('" . 'Update record ok' . "')");
    }
} else {
    $db->query("insert into `debug`(`text`) values('" . "Upload failed." . "')");
}
$db->query("insert into `debug`(`text`) values('" . $file_msg . "')");


