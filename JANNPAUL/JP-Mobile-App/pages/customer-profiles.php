<!-- @@TITLE=Registered Users-->
<!-- @@DESCRIPTION=All registered users-->
<!-- @@BREADCRUMB=Profile Management#Registered Users-->
<div class="row">
    <div class="col-xs-12">
        <div class="box">
            <div class="box-body">
                <table id="users" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Avatar</th>
                            <th>Email</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Member Since</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th>Avatar</th>
                            <th>Email</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Member Since</th>
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
        <!--<a href="./?profile" class="btn btn-info pull-right"><i class="fa fa-plus"></i> New user</a>-->
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
        var table = $('#users').DataTable({
            processing: true,
            serverSide: true,
            ajax: "../app/api/customer-profiles.php",
            searching: true,
            ordering: true,
            select: true,
            stateSave: true,
            columns: [
                {name: 'picture', orderable: false, searchble: false},
                {name: 'email'},
                {name: 'fname'},
                {name: 'lname'},
                {name: 'member_since'}
            ]
        });
    });
</script>