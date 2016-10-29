<?php

include 'api-header.php';
$default_condition = " WHERE (`product_id_1` = " . filter_input(INPUT_GET, "product_id") . ")";
$condition = $default_condition;
$order_by = "";

$sql = "SELECT * FROM `similar_product` join `product` on `similar_product`.`product_id_2` = `product`.`product_id` $condition $order_by";
if ($start >= 0 && $length > 0) {
    $sql .= " LIMIT $start, $length";
}
$result = $db->query($sql);
$data = "";

$count_result = $db->query("SELECT COUNT(*) count FROM `similar_product` $default_condition");
$count_obj = 0;
if ($count_result) {
    $count_result->fetch_object()->count;
    $count_result->close();
}

if ($result) {
    while ($similar_product = $result->fetch_object()) {
        $image_url = '';
        if ($similar_product->picture !== "") {
            $image_url = $similar_product->picture;
        } else {
            $image_url = './dist/img/default-50x50.gif';
        }
        if ($data !== "") {
            $data .= ",";
        }

        $data .= '["' .
                '<img class=\\"img-circle\\" style=\\"width: 45px;height: 45px;\\" src=\\"' .
                $image_url . '\\" />","' .
                '<a href=\\"./?product=' . $similar_product->product_id . '\\">' . $similar_product->title . '</a>", '
                . '"<input type=\\"button\\" onclick=\\"deleteSimilarProduct(' . $similar_product->product_id_1 . ',' . $similar_product->product_id_2 . ');\\" class=\\"btn btn-block btn-danger btn-xs\\" value=\\"remove\\" />"' .
                ']';
    }
    $result->close();
}

$json = '{
   "draw":' . filter_input(INPUT_GET, 'draw') . ',
   "recordsTotal":' . $count_obj . ',
   "recordsFiltered":' . $count_obj . ',
   "data":[ ' . $data . '
   ]
}';

echo $json;
