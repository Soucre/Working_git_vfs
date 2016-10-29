<!DOCTYPE html>
<?php
require_once '../app/system/config.php';
require_once '../app/api/database.php';
$title = filter_input(INPUT_GET, "t");
if (!isset($title)) {
    $title = "(JANNPAUL)";
}
$description = filter_input(INPUT_GET, "d");
if (!isset($description)) {
    $description = "";
}
$img = filter_input(INPUT_GET, "k");
if (!isset($img)) {
    $img = "../dist/img/solasfera-round.png";
}
?>

<html lang="en" class=" js csstransforms csstransforms3d csstransitions"><head>
        <title><?php echo htmlspecialchars($title); ?></title>
        <meta charset="UTF-8">
    </head>
    <style>
        img.fill {
            display: block;
            max-width:100%;
            width: auto;
            height: auto;
        }
        img.cell {
            display: block;
            max-width:50%;
            width: auto;
            height: auto;
        }
        body {
            font-family: 'Open Sans', sans-serif;
            font-size: 12px;
            margin: 32px;
        }
        h1 {
            font-size: 18px;
            text-align: center;
            font-weight: normal;
        }
        h2 {
            font-size: 16px;
            text-align: center;
            font-weight: normal;
        }
        span.midle {
            display: table-cell;
            vertical-align: middle;
            margin-left: 10px;
        }
        span.title{
            font-size: 16px;
            text-align: center;
        }
        p.center {
            text-align: center;
        }
        img.centered {
            text-align: center;
        }
        h3 {
            margin-bottom: 28px;
            margin-top: -32px;
            font-size: 1.4em;
            text-align: center;
            font-weight: normal;
        }

        /* Portrait and Landscape */
        @media only screen 
        and (min-device-width: 768px) 
        and (max-device-width: 1024px) 
        and (-webkit-min-device-pixel-ratio: 1) {
            h3 {
                margin-top: 28px;
            }
        }
    </style>
    <body> 
        <h3><?php echo htmlspecialchars($title); ?></h3>
        <img class="fill" src="<?php echo $img; ?>"/>
        <p>
            <?php echo htmlspecialchars($description); ?>
        </p>

    </body>
</html>