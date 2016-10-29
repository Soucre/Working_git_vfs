<html lang="en" class=" js csstransforms csstransforms3d csstransitions"><head>
        <title>JANNPAUL UNIQUE SERIES</title>
        <meta charset="UTF-8">
        <meta name="author" content="http://saigontechcom.vn">
        <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
    </head>
    <style>
        img.fill {
            display: block;
            max-width:100%;
            width: 76%;
            height: auto;
            margin: auto;
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
        <?php if (filter_input(INPUT_GET, "page") == "1") { ?>
            <h3>Super Ideal Cut</h3>
            <img class="fill" src="../../dist/img/super-ideal-cut.png"/>
            <p>
                JP Super Ideal Rounds are the traditional 57 faceted, 8 Hearts & Arrows diamond that is cut to the most exacting standard. Each Super Ideal Cut diamond goes through our stringent quality selection, involving several demanding criteria.  
            </p><p>
                All of our diamonds are handpicked to ensure that each is cut to the maximum light performance and is guaranteed to outperform any other 57 facets round.
            </p><p>
                JANNPAUL utilizes the latest technology such as the HCA tool, Ideal scope, ASET scope and Hearts & Arrows scope to analyze each diamond. No other jewellers in the world follow the same ambitious standards as JANNPAUL. 
            </p><p>
                Only a handful of diamonds are able to meet our uncompromising requirements. When you receive a JP Super Ideal Round diamond, you are guaranteed the most brilliant diamond. 
            </p>

        <?php } else if (filter_input(INPUT_GET, "page") == "2") { ?>
            <h3>Cushion Brellia</h3>
            <img class="fill" src="../../dist/img/cushion-brellia.png"/>
            <p>
                The Cushion Brellia is the world's first Cushion Hearts & Arrows diamond. The sheer brightness of the Brellia even surpasses most rounds. It was made to have such brightness to enhance the whiteness, emphasizing on the arrows. It proved to be the most brilliant Cushion diamond to have ever been cut.
            </p><p>
                The Brellia has 57 facets in virtually the same pattern as the finest Super Ideal cut round, retaining the same scintillation.The pavilions (Arrows) of the Brellia are designed wider to create large bold flashes of fire that can be seen at further distance. 
            </p><p>
                Each Brellia diamond is cut and polished to the most exacting standards on earth. A standard known as "Super Ideal Cuts". It achieves the perfection of hearts and arrows with an AGS 000 cut grade, resulting in the highest brilliancy.
            </p>
        <?php } else if (filter_input(INPUT_GET, "page") == "3") { ?>
            <h3>Octagon Hearts & Arrows</h3>
            <img class="fill" src="../../dist/img/octagon.png"/>
            <p>
                We are proud to introduce the world's first Octagon Hearts & Arrows diamond, also known as the D'amor.
                The Octagon Hearts & Arrows is intricately cut with 88 facets, 8 sides, 8 Hearts and 8 Arrows. 
            </p><p>
                The 88 facets increases the scintillation, creating a mesmerizing dance in the sparkle when you move your diamond ring finger. 
            </p><p>
                Despite the higher facets, it manages to retain the traditional 8 Hearts & Arrows patterning, maximizing the fire and colorful burst of light.It achieves the best of both worlds, optimizing the fire and scintillation, resulting in the most brilliant diamond in the world.
            </p><p>
                The unique shape of the Octagon adds playfulness to the diamond. It allows the wearer to create his/her own look. It can be set into a more squarish or roundish feature by simply rotating the diamond. The extraordinary octagon shape with the 8 perfectly symmetrical sides highlights the strong identity of the diamond.
            </p>
        <?php } else if (filter_input(INPUT_GET, "page") == "4") { ?>
            <h3>Solasfera Round</h3>
            <img class="fill" src="../../dist/img/solasfera-round.png"/>
            <p>
                The Solasfera Round is one of the very few modified round diamonds that manages to retain its Hearts & Arrows patterning. It is cut with an astounding 91 facets and 10 Hearts & Arrows. a
            </p><p>
                The exacting symmetry of Solasfera's cuts, which are crafted with unparalleled precision, creates a perfect optical patterning that makes a diamond's sparkle visible from afar. It is cut to obtain the maximum light return, showing a full red on the Ideal Scope, intensifying the brightness of the diamond.
            </p><p>
                The additional Hearts & Arrows patterning in the diamond creates a breathtaking scintillation. Unlike others in the industry, Solasfera diamonds are always cut to maximize beauty, not size. The most breathtaking diamonds exude the greatest brilliance (white light), fire (color light) and sparkle (scintillation). 
            </p><p>
                Every Solasfera diamond must measure off the charts in all three categories to earn its name. Each Solasfera comes with an independent light performance lab report (Gemex), to certify that it attains the highest possible grading in all 3 categories. 
            </p>
        <?php } ?>
    </body>
</html>