<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

class Variant {
    public $count = 0;
    public $data = Array();
    
    public function __construct() {
        
        $db = database\getMySqli();
        
        $result = $db->query("SELECT * FROM `product_variant` ORDER BY `carat`, `diamond_cut`, `setting`");
        if ($result) {
            $this->count = $result->num_rows;
            while($variant = $result->fetch_object()) {
                if($variant->try_it_on_picture) {
                    $variant->try_it_on_picture = "http://$_SERVER[HTTP_HOST]/$variant->try_it_on_picture";
                }
                // Pictures
                $variant_id = $variant->variant_id;
                $pictures_result = $db->query("SELECT * from `product_variant_picture` where `variant_id` = $variant_id");
                
                $variant->pictures = Array();
                if($pictures_result && $pictures_result->num_rows > 0) {
                    while($picture = $pictures_result->fetch_object()) {
                        if($picture->picture) {
                            array_push($variant->pictures, "http://$_SERVER[HTTP_HOST]/$picture->picture");
                        }
                    }
                }
                
                array_push($this->data, $variant);
                if($pictures_result) {
                    $pictures_result->close();
                }
            }
        }
    }

}

$variant = new Variant();
echo json_encode($variant);

