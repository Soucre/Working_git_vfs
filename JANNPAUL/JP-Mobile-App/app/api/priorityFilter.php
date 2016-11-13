<?php

require_once dirname(__DIR__) . '/system/config.php';
require_once dirname(__DIR__) . '/system/utils.php';
require_once __DIR__ . '/database.php';

class Variant {
    public $count = 0;
    public $data = Array();
    
    public function __construct() {
        
        $db = database\getMySqli();
        $priority_id = filter_input(INPUT_GET, "priority");
		$priority_Name = "";
		if($priority_id == 1){
			$priority_Name = "Super%Ideal";
		}
		
		if($priority_id == 2){
			$priority_Name = "Solasfera%Round";
		}
		
		if($priority_id == 3){
			$priority_Name = "Cushion%Brellia";
		}
		
		if($priority_id == 4){
			$priority_Name = "Octagon%Amor";
		}
		
		
        $result = $db->query("SELECT 
								pv.variant_id,
								pv.product_id, 
								pv.is_default, 
								pd.visible,  
								pv.carat, 
								pv.diamond_cut , 
								pd.title, 
								pv.priority
							FROM product_variant pv
							INNER JOIN product pd ON pv.product_id = pd.product_id
							WHERE pv.diamond_cut like '".$priority_Name."'
							AND pd.visible = 1
							and is_default = 1 ORDER BY pd.GroupId");
        if ($result) {
			
            $this->count = $result->num_rows;
            while($variant = $result->fetch_object()) { 
			
				$variant->priority = "<input id='priority". $variant->variant_id ."' size='2' min='1' max='50' maxlength=2 style='width : 50px' type='number' value=". $variant->priority ." >";
				
				$variant->saveAction = "<input id='btnSave". $variant->variant_id ."' type='button' onclick='savePriority(". $variant->variant_id .");' class='btn btn-block btn-danger btn-xs' value='save'>";
				
                array_push($this->data, $variant);
            }
        }
    }

}

$variant = new Variant();
echo json_encode($variant);

