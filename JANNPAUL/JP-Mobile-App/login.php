<?php
session_start();
require_once './app/system/config.php';
require_once './app/api/database.php';
?>

<!DOCTYPE html>
<html>
    <head>
        <?php include './app/head.php'; ?>
        <!-- iCheck -->
        <link href="./plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
    </head>
    <body class="login-page">
        <div class="login-box">
            <div class="login-logo">
                <a href="#"><?php echo $_SESSION[LOGO_LG]; ?></a>
            </div><!-- /.login-logo -->
            <div class="login-box-body">
                <p class="login-box-msg"><?php echo $_SESSION[MEMBER_LOG_IN_MSG]; ?></p>
                <form action="./" method="post">
                    <div class="form-group has-feedback">
                        <input id="email" name="email" type="email" class="form-control" placeholder="Email" />
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <input id="password" name="password" type="password" class="form-control" placeholder="Password" />
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="row">
                        <div class="col-xs-8">
                            <div class="checkbox icheck">
                                <label>
                                    <input type="checkbox" id="remember-me" name="remember-me" value="1"> Remember Me
                                </label>
                            </div>
                        </div><!-- /.col -->
                        <div class="col-xs-4">
                            <button type="submit" class="btn btn-info btn-block btn-flat">Sign In</button>
                        </div><!-- /.col -->
                    </div>
                </form>

                <div class="social-auth-links text-center">
                    <p>- OR -</p>
                    <!--<a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
                    <a href="#" class="btn btn-block btn-social btn-google-plus btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>
                    -->
                </div><!-- /.social-auth-links -->

                <a href="#">I forgot my password</a><br>
                <!--<a href="register.html" class="text-center">Register a new membership</a>-->

            </div><!-- /.login-box-body -->
        </div><!-- /.login-box -->

        <!-- jQuery 2.1.4 -->
        <script src="../../plugins/jQuery/jQuery-2.1.4.min.js" type="text/javascript"></script>
        <!-- Bootstrap 3.3.2 JS -->
        <script src="../../bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <!-- iCheck -->
        <script src="../../plugins/iCheck/icheck.min.js" type="text/javascript"></script>
        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });
        </script>
    </body>
</html>
