<!-- @@TITLE=(Diamond cut)-->
<!-- @@DESCRIPTION=Please fill in product's information-->
<!-- @@BREADCRUMB=<a href="?products">Products</a>#Product-->
<?php

include './app/form-notification.php';
include "./app/api/variant-helper.php";
$priority_Name = "";
$priorityName = filter_input(INPUT_GET, "priority");
	if($priorityName == 1){
			$priority_Name = "Super Ideal";
		}
		
		if($priorityName == 2){
			$priority_Name = "Solasfera Round";
		}
		
		if($priorityName == 3){
			$priority_Name = "Cushion Brellia";
		}
		
		if($priorityName == 4){
			$priority_Name = "Octagon D'Amor";
	}
echo '<script>contentHeaderTitle_SetContent("' . htmlspecialchars($priority_Name) . '<small></small>")'
    . '</script>';
?>

<!-- Select2 -->
<link href="plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
<style>
    td.details-control {
        background: url('https://www.datatables.net/examples/resources/details_open.png') no-repeat center center;
        cursor: pointer;
    }
    tr.shown td.details-control {
        background: url('https://www.datatables.net/examples/resources/details_close.png') no-repeat center center;
    }
</style>
<img src="./dist/img/loading.gif" id="loading-indicator" style="display:none" />
<style>
    #loading-indicator {
        width: 45px;
        height: 45px;
        position: fixed;
        top: 50%;
        left: 50%;
        /* bring your own prefixes */
        transform: translate(-50%, -50%);
        z-index: 1000;
    }
</style>
<form role="form" action="./?product=<?php echo $product_id; ?>" method="POST" enctype="multipart/form-data">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-default">
                   
                    <div class="box-body">
                        <table id="variants" class="table table-bordered table-hover">
                            <thead>
                                <tr>                                    
                                    <th>Title</th>
                                    <th>Carat</th>
                                    <th>Diamond Cut</th>                                
                                    <th>Priority</th>
                                    <th></th>
                                </tr>
                            </thead>
                        </table>
                    </div><!-- /.box-body -->
                    
                </div><!-- /.box -->
            </div>
        </div>
    
   
</form>

<!-- DATA TABLES -->
<link href="./plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
<!-- DATA TABES SCRIPT -->
<script src="./plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="./plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
<!-- Select2 -->
<script src="./plugins/select2/select2.full.min.js" type="text/javascript"></script>
<!-- page script -->
<script type="text/javascript">                         
	$(document).ready(function () {
		$('#variants').DataTable({
			processing: true,
			serverSide: true,
			ajax: "../app/api/priorityFilter.php?priority=<?php echo $priorityName; ?>",
			"searching": false,
			"stateSave": true,
			"paging": false,
			"ordering": false,
			"info": false,
			columns: [			
                    { data: 'title' },
                    { data: 'carat' },
                    { data: 'diamond_cut' },
                    { data: "priority" },
                    { data: "saveAction" }
			]
		});
		
	});					  
    function savePriority(variant_id) {
			var priorityValue = $('#priority'+ variant_id).val(); // must be validate
			var isCheck= false;
			if(!priorityValue || priorityValue <= 0){
				priorityValue = 1;
				isCheck = true;
			}
			
			if(priorityValue > 50 ){
				priorityValue = 50;
				isCheck = true;
			}
			
			$.ajax({
				url: '/app/api/udpate-variant-byPriority.php?variant_id=' + variant_id + "&priority=" + priorityValue,
				type: 'GET',
				//Options to tell JQuery not to process data or worry about content-type
				cache: false,
				contentType: false,
				processData: false,
				success: function (data) {						
					if(data)
					{
						if(data = "1")
							$("#btnSave" + variant_id).attr('class', "btn btn-block btn-success btn-xs");
						else
							$("#btnSave" + variant_id).attr('class', "btn btn-block btn-danger btn-xs");
						
						if(isCheck){
							$('#priority'+ variant_id).val(priorityValue);
						}
							
					}
				}
			});
		
        };
                            
</script>