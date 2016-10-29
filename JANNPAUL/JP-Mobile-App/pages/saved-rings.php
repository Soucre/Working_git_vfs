<!-- @@TITLE=Saved Rings-->
<!-- @@DESCRIPTION=Rings have been saved by the users-->
<!-- @@BREADCRUMB=Saved Rings-->
<div class="row">
    <div class="col-xs-12">
        <div class="box">
            <div class="box-header">
                <h3 class="box-title">All Saved Rings</h3>
            </div><!-- /.box-header -->
            <div class="box-body">
                <table id="products" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Category</th>
                            <th>Metal</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Category</th>
                            <th>Metal</th>
                        </tr>
                    </tfoot>
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
        var table = $('#products').DataTable({
            processing: true,
            serverSide: true,
            ajax: "../app/api/saved-rings.php",
            searching: true,
            ordering: true,
            stateSave: true,
            columns: [
                {name: 'picture', orderable: false, searchble: false},
                {
                    name: 'title'
                }
                ,
                {
                    name: 'category'
                }
                ,
                {
                    name: 'metal'
                }
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