<?php

include 'api-header.php';
$condition = "";
$order_by = "";

if (isset($search) && $search['value']) {
    $search_value = $db->real_escape_string($search['value']);
    $condition = "WHERE (`title` LIKE '%$search_value%') or (`description` LIKE '%$search_value%')  or (`category` LIKE '%$search_value%')";
}

if (isset($order) && isset($order[0]['column'])) {
    $order_by = "ORDER BY " . $columns[$order[0]['column']]['name'] . " " . $order[0]['dir'];
}

$result = $db->query("SELECT `product`.*, (select `title` from `category` where `category`.`category_id` = `product`.`category`) as `category` FROM `product` JOIN `saved_ring` ON `product`.`product_id` = `saved_ring`.`product_id` $condition $order_by LIMIT $start, $length");
$data = "";

$count_result = $db->query("SELECT COUNT(*) count FROM `saved_ring`");
$count_obj = $count_result->fetch_object();

$filter_count_result = $db->query("SELECT COUNT(*) count FROM `saved_ring` $condition");
$filter_count_obj = $filter_count_result->fetch_object();

if ($result) {    
    while ($product = $result->fetch_object()) {
        $image_url = '';
        if ($product->picture !== "") {
            $image_url = $product->picture;
        } else {
            $image_url = './dist/img/default-50x50.gif';
        }
        if ($data !== "") {
            $data .= ",";
        }

        $data .= '["' .
                '<img class=\\"img-circle\\" style=\\"width: 45px;height: 45px;\\" src=\\"' .
                $image_url . '\\" />","' .
                '<a href=\\"./?product=' . $product->product_id . '\\">' . \utils\jsonEscapseString($product->title) . '</a>' . '","' .
                \utils\jsonEscapseString($product->category) . '","' .
                \utils\jsonEscapseString($product->metal) . '"' .
                ']';
    }
    $result->close();
}

$json = '{
   "draw":' . filter_input(INPUT_GET, 'draw') . ',
   "recordsTotal":' . $count_obj->count . ',
   "recordsFiltered":' . $filter_count_obj->count . ',
   "data":[ ' . $data . '
   ]
}';

$count_result->close();
$filter_count_result->close();

echo $json;
