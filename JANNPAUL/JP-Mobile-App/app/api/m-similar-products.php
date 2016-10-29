<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

class SimilarProduct {

    public $count = 0;
    public $data = Array();

    public function __construct() {

        $db = database\getMySqli();

        $result = $db->query("SELECT * from `similar_product`");
        if ($result) {
            $this->count = $result->num_rows;
            while ($similar_product = $result->fetch_object()) {
                array_push($this->data, $similar_product);
            }
        }
    }

}

$similar_product = new SimilarProduct();

echo json_encode($similar_product);
