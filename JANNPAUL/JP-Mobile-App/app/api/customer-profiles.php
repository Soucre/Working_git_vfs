<?php

include 'api-header.php';
$condition = "";
$order_by = "";

if (isset($search) && $search['value']) {
    $search_value = $db->real_escape_string($search['value']);
    $condition = "WHERE (`fname` LIKE '%$search_value%') or (`lname` LIKE '%$search_value%') or (`email` LIKE '%$search_value%')  or (`address` LIKE '%$search_value%')";
}

if (isset($order) && isset($order[0]['column'])) {
    $order_by = "ORDER BY " . $columns[$order[0]['column']]['name'] . " " . $order[0]['dir'];
}

$result = $db->query("SELECT * FROM `customer_profile` $condition $order_by LIMIT $start, $length");
$data = "";

$count_result = $db->query("SELECT COUNT(*) count FROM `customer_profile`");
$count_obj = 0;
if ($count_result) {
    $count_obj = $count_result->fetch_object()->count;
    $count_result->close();
}

$filter_count_result = $db->query("SELECT COUNT(*) count FROM `customer_profile` $condition");
$filter_count_obj = 0;
if ($filter_count_result) {
    $filter_count_obj = $filter_count_result->fetch_object()->count;
    $filter_count_result->close();
}

if ($result) {
    while ($member = $result->fetch_object()) {
        $image_url = '';
        if ($member->picture && $member->picture !== "" && strlen($member->picture) > 0) {
            $image_url = $member->picture;
        } else {
            $image_url = './dist/img/avatar.png';
        }
        if ($data !== "") {
            $data .= ",";
        }

        $data .= '["' .
                '<img class=\\"img-circle\\" style=\\"width: 45px;height: 45px;\\" src=\\"' .
                $image_url . '\\" />","' .
                '<a href=\\"./?customer-profile=' . $member->mem_id . '\\">#' . $member->email . '</a>' . '","' .
                \utils\jsonEscapseString($member->fname) . '","' .
                \utils\jsonEscapseString($member->lname) . '","' .
                utils\dateToString($member->member_since) . '"' .
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
