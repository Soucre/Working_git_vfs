<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

class Product {

    public $count = 0;
    public $data = Array();

    public function __construct() {

        $db = database\getMySqli();
        
        $result = $db->query("SELECT `category`, `product_id`, `title`, 
            `metal`, `picture`,  `description`, `sharing_url`
            FROM `product` 
             WHERE `product`.`visible` = 1 ORDER BY `title`");
        if ($result) {
            $this->count = $result->num_rows;
            while ($product = $result->fetch_object()) {
                if ($product->picture) {
                    $product->picture = "http://$_SERVER[HTTP_HOST]/$product->picture";
                }
                $product->description = strip_tags($product->description);
                
                array_push($this->data, $product);
            }
        }
    }

}

$product = new Product();

echo json_encode($product);
