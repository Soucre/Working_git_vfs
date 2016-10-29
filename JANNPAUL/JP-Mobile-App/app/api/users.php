<?php

include 'api-header.php';
$condition = "";
$order_by = "";

if (isset($search) && $search['value']) {
    $search_value = $db->real_escape_string($search['value']);
    $condition = "WHERE (`fname` LIKE '%$search_value%') or (`lname` LIKE '%$search_value%') or (`email` LIKE '%$search_value%') or (`address` LIKE '%$search_value%')";
}

if (isset($order) && isset($order[0]['column'])) {
    $order_by = "ORDER BY " . $columns[$order[0]['column']]['name'] . " " . $order[0]['dir'];
}

$result = $db->query("SELECT * FROM `member` $condition $order_by LIMIT $start, $length");
$data = "";

$count_result = $db->query("SELECT COUNT(*) count FROM `member`");
$count_obj = $count_result->fetch_object();

$filter_count_result = $db->query("SELECT COUNT(*) count FROM `member` $condition");
$filter_count_obj = $filter_count_result->fetch_object();

if ($result) {    
    while ($member = $result->fetch_object()) {
        $image_url = \utils\getAvatarIfAvailableOrShowDefaultIfNull($member->picture);
        
        if ($data !== "") {
            $data .= ",";
        }

        $data .= '["' . 
                '<img class=\\"img-circle\\" style=\\"width: 45px;height: 45px;\\" src=\\"' .
                $image_url . '\\" />","' .
                '<a href=\\"./?profile=' . $member->mem_id . '\\">#' . $member->email . '</a>' . '","' .
                \utils\jsonEscapseString($member->fname) . '","' .
                \utils\jsonEscapseString($member->lname) . '","' .
                utils\dateToString($member->member_since) . '"' .
                ']';
    }
}

$json = '{
   "draw":' . filter_input(INPUT_GET, 'draw') . ',
   "recordsTotal":' . $count_obj->count . ',
   "recordsFiltered":' . $filter_count_obj->count . ',
   "data":[ ' . $data . '
   ]
}';

$result->close();
$count_result->close();
$filter_count_result->close();

echo $json;
