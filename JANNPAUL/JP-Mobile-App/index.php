<?php

session_start();
require_once './app/system/config.php';
require_once './app/system/utils.php';
require_once './app/api/database.php';
require_once './app/system/settings.php';
require_once './app/system/image-utils.php';

//-------------------------------------------------------------------------------------------------------------------------------------------------------
//require_once './app/api/m-upload-try-it-on-user-image.php';
//-------------------------------------------------------------------------------------------------------------------------------------------------------

require_once './app/system/authenticate.php';
include './app/content-navigation.php';
?>

<!DOCTYPE html>
<html>
    <head>
        <?php include './app/head.php'; ?>
        <!-- AdminLTE Skins. Choose a skin from the css/skins
             folder instead of downloading all of them to reduce the load. -->
        <link href="dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />   
        <!-- iCheck -->
        <link href="plugins/iCheck/flat/blue.css" rel="stylesheet" type="text/css" />
        <!-- Morris chart -->
        <link href="plugins/morris/morris.css" rel="stylesheet" type="text/css" />
        <!-- jvectormap -->
        <link href="plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
        <!-- Date Picker -->
        <link href="plugins/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />
        <!-- Daterange picker -->
        <link href="plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
        <!-- bootstrap wysihtml5 - text editor -->
        <link href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" type="text/css" />
        <!-- ChartJS 1.0.1 -->
        <script src="plugins/chartjs/Chart.min.js" type="text/javascript"></script>
    </head>
    <body class="skin-black sidebar-mini">
        <style>
            td {
                vertical-align: middle !important;
            }
        </style>

        <div class="wrapper">
            <!-- Main Header -->
            <header class="main-header">

                <!-- Logo -->
                <a href="./" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><?php echo $_SESSION[LOGO_MINI]; ?></span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><?php echo $_SESSION[LOGO_LG]; ?></span>
                </a>

                <!-- Header Navbar -->
                <nav class="navbar navbar-static-top" role="navigation">
                    <!-- Sidebar toggle button-->
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <?php include "./app/navbar-right-menu.php"; ?>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">

                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">

                    <?php include './app/sidebar-user-panel.php'; ?>

                    <?php include './app/search-form.php'; ?>

                    <?php include './app/sidebar-menu.php'; ?>
                </section>
                <!-- /.sidebar -->
            </aside>

            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <?php include './app/content-header.php'; ?>

                <!-- jQuery 2.1.4 -->
                <script src="plugins/jQuery/jQuery-2.1.4.min.js" type="text/javascript"></script>
                <!-- Select2 -->
                <script src="plugins/select2/select2.full.min.js" type="text/javascript"></script>
                <!-- InputMask -->
                <script src="plugins/input-mask/jquery.inputmask.js" type="text/javascript"></script>
                <script src="plugins/input-mask/jquery.inputmask.date.extensions.js" type="text/javascript"></script>
                <script src="plugins/input-mask/jquery.inputmask.extensions.js" type="text/javascript"></script>
                <!-- date-range-picker -->
                <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.2/moment.min.js" type="text/javascript"></script>
                <script src="plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
                <section class="content">
                    <?php
                    if (authenticate\auth_can_read($_SESSION[PAGE])) {
                        include $_SESSION[PAGE];
                    } else {
                        $_SESSION[APPLICATION_ERROR_CODE] = 401; // Unauthenticated
                        include './pages/error-page.php';
                    }
                    ?>
                </section><!-- /.content -->
            </div><!-- /.content-wrapper -->

            <!-- Main Footer -->
            <footer class="main-footer">
                <!-- To the right -->
                <div class="pull-right hidden-xs">
                    <?php echo $_SESSION[SLOGAN]; ?>
                </div>
                <!-- Default to the left -->
                <strong>Copyright &copy; <?php echo $_SESSION[COPY_RIGHTS]; ?> <a href="<?php echo $_SESSION[WEBSITE]; ?>"><?php echo $_SESSION[COMPANY]; ?></a>.</strong> All rights reserved.
            </footer>

            <?php include './app/control-sidebar.php'; ?>

        </div><!-- ./wrapper -->

        <!-- REQUIRED JS SCRIPTS -->

        <!-- Bootstrap 3.3.2 JS -->
        <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js" type="text/javascript"></script>
    </body>
</html>
