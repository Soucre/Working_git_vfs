<?php if (isset($_SESSION[APPLICATION_MESSAGE])) { ?>
    <div style="display: none;" id="form-notifivation-div" class="alert alert-<?php echo $_SESSION[APPLICATION_MESSAGE_TYPE]; ?> alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4>	<i class="icon fa fa-check"></i> <?php
            if ($_SESSION[APPLICATION_MESSAGE_TYPE] === "info") {
                echo "Info";
            } else if ($_SESSION[APPLICATION_MESSAGE_TYPE] === "warning") {
                echo "Warning";
            } else if ($_SESSION[APPLICATION_MESSAGE_TYPE] === "success") {
                echo "Success";
            } else if ($_SESSION[APPLICATION_MESSAGE_TYPE] === "danger") {
                echo "Danger";
            }
            ?></h4>
        <?php echo $_SESSION[APPLICATION_MESSAGE]; ?>
    </div>

    <script>
        $(document).ready(function () {
            $("#form-notifivation-div").slideDown("slow", function () {
                // Animation complete.
            });
        });
    </script>
    <?php
}

unset($_SESSION[APPLICATION_MESSAGE]);
unset($_SESSION[APPLICATION_MESSAGE_TYPE]);
