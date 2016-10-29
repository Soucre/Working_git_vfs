<?php

include 'api-header.php';
$default_condition = " WHERE (`product_id` = " . filter_input(INPUT_GET, "product_id") . ")";
$condition = $default_condition;
$order_by = "";

$sql = "SELECT * FROM `product_variant` $condition $order_by";
if ($start >= 0 && $length > 0) {
    $sql .= " LIMIT $start, $length";
}
$result = $db->query($sql);
$data = "";

$count_result = $db->query("SELECT COUNT(*) count FROM `product_variant` $default_condition");
$count_obj = 0;
if ($count_result) {
    $count_result->fetch_object()->count;
    $count_result->close();
}

$filter_count_result = $db->query("SELECT COUNT(*) count FROM `product_variant` $condition");
$filter_count_obj = 0;
if ($filter_count_result) {
    $filter_count_result->fetch_object()->count;
    $filter_count_result->close();
}

if ($result) {
    while ($variant = $result->fetch_object()) {
        $image_url = '';
        if ($variant->try_it_on_picture !== "") {
            $image_url = $variant->try_it_on_picture;
        } else {
            $image_url = './dist/img/default-50x50.gif';
        }
        if ($data !== "") {
            $data .= ",";
        }

        $pictures_data = "";

        $picture_query = $db->query("SELECT * from `product_variant_picture` WHERE `variant_id` = $variant->variant_id");

        $found = false;
        if ($picture_query) {
            $pictures_data .= "<tr><td>";
            while ($picture = $picture_query->fetch_object()) {
                $found = true;
                $pictures_data .= "<div style=\\\"display: inline-block; padding: 5px;\\\"> <img src=\\\"$picture->picture\\\" alt=\\\"Picture\\\" style=\\\"width: 100%;max-width: 85px;height: auto;\\\" /></div>";
            }
            $picture_query->close();
            $pictures_data .= "</td></tr>";
        }

        $msg = "No additional pictures. ";
        if ($found) {
            $msg = "";
        }
        $pictures_data .= "<tr><td><input id=\\\"add_variant_picture_" . $variant->variant_id . "\\\" style=\\\"display: none;\\\" type=\\\"file\\\" accept=\\\"image/gif, image/jpeg, image/png\\\">$msg<a href=\\\"#\\\" onclick=\\\"addVariantPicture(" . $variant->variant_id . "); return false;\\\">Add</a> new picture?</td></tr>";

       $data .= '["' . $pictures_data . '", "' .
                \utils\jsonEscapseString($variant->title) . '","' .
                $variant->carat . ' Carat","' .
                $variant->diamond_cut . '","' .
                $variant->setting . '","' .
                '<input id=\\"try_it_on_upload_' . $variant->variant_id . '\\" style=\\"display: none;\\" type=\\"file\\" accept=\\"image/png\\"> <a href=\\"javascript:void(0);\\" onclick=\\"changeTryItOnPicture(' . $variant->variant_id . ');\\"><img id=\\"try_it_on_pic_' . $variant->variant_id . '\\" class=\\"img-circle\\" style=\\"width: 45px;height: 45px;\\" src=\\"' .
                $image_url . '\\" /></a>", "<input type=\\"button\\" onclick=\\"deleteVariant(' . $variant->variant_id . ');\\" class=\\"btn btn-block btn-danger btn-xs\\" value=\\"remove\\" />"' .
                ']';
       
        
    }
    $result->close();
}

$json = '{
   "draw":' . filter_input(INPUT_GET, 'draw') . ',
   "recordsTotal":' . $count_obj . ',
   "recordsFiltered":' . $filter_count_obj . ',
   "data":[ ' . $data . '
   ]
}';

echo $json;
