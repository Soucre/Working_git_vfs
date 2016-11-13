<!-- @@TITLE=Diamond cut-->
<!-- @@DESCRIPTION=List of all diamond cut-->
<!-- @@BREADCRUMB=Products-->
<div class="row">
    <div class="col-xs-12">
        <div class="box">
            <div class="box-header">
                <h3 class="box-title">All Type</h3>
            </div><!-- /.box-header -->
            <div class="box-body">
                <table id="cutList" class="table table-bordered table-hover">
                    <thead>
                        <tr>                            
                            <th>Name</th>                            
                        </tr>
                    </thead>                    
                </table>
            </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->    
</div><!-- /.row -->    
<!-- Buttons -->
<div class="row no-print">
    <div class="col-xs-12">
        <a href="#" target="_blank" class="btn btn-default disabled"><i class="fa fa-print"></i> Print</a>        
        <!--<button class="btn btn-info pull-right" style="margin-right: 5px;"><i class="fa fa-download"></i> Generate PDF</button>-->
    </div>
</div>
<!-- DATA TABLES -->
<link href="./plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
<!-- DATA TABES SCRIPT -->
<script src="./plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="./plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
<!-- page script -->
<script type="text/javascript">
    $(document).ready(function () {
		var data = [
			["<a href=\"/?priority=1\">Super Ideal<a>"],
			["<a href=\"/?priority=2\">Solasfera Round"],
			["<a href=\"/?priority=3\">Cushion Brellia"],
			["<a href=\"/?priority=4\">Octagon D'Amor"]
		];
		
        var table = $('#cutList').DataTable({
			data: data,          
            ordering: false,
			"searching": false,
            columns: [
                { name: 'Name' }
            ]
        });
        /*table.on('select', function (e, dt, type, indexes) {
            alert('something');

            if (type === 'rows') {
                var data = table.rows(indexes).data().pluck('id');
                // do something with the ID of the selected items
            }
        });*/
    });
</script>