<html lang="en" class=" js csstransforms csstransforms3d csstransitions"><head>
        <title>JANNPAUL DIAMONDS</title>
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
        <?php if (filter_input(INPUT_GET, "page") == "1") { ?>
            <h3>PAST</h3>
            <img class="fill" src="../../dist/img/house-of-hung.png"/>
            <p>
                House of Hung was founded in 1973 as a family business and was one of the very few jewellery stores in Singapore. The first store was located in Tanglin Shopping Centre, along the Orchard road shopping district. In the beginning, they offered electronic goods and jewellery products. 
            </p>
            <p>
                In 1975, a decision was made to specialise in finer jewelleries and an in-house team of skilled craftsmen. They provided customization services with traditional hand-sketched drawings. In that same year, the store was relocated to Far East Shopping Centre and was the largest jewellery store in Singapore. 
            </p>
            <p>
                Over the years, they established themselves to be one of Singapore's largest diamond wholesalers and manufacturer. Till date, it still operates in the same location and offers an extensive range of gemstones and jewellery products. Later, this is where JannPaul was conceptualize with the vision to specialize in Super Ideal Cut diamonds.
            </p>

        <?php } else if (filter_input(INPUT_GET, "page") == "2") { ?>
            <h3>PRESENT</h3>
            <img class="fill" src="../../dist/img/now.png"/>
            <p>
                In 2010, JannPaul was founded to provide consumers high performing diamonds at competitive prices. The company was established due to the lack of transparency in the diamond industry. 
            </p><p>
                Buying a diamond based on a certificate is insufficient and consumers often had to rely on the words of a salesperson to judge a diamond's quality. There was a lack of information in the market and consumers were at a disadvantage. The Idea behind JannPaul was to provide consumers a comprehensive diamond education sessions to learn more about diamonds and the industry. We shared trade secrets and the Dos and Don'ts in purchasing a diamond.
            </p><p>
                JannPaul was proud to introduce the Super Ideal Cut standard in Singapore in 2011. We had the strictest set of criteria that was unsurpassed by any others. In addition, we were the only store that had in-house CAD designers to customise jewellery with a realistic 3D preview. Clients were able to view their 3D renders being designed live while sharing their thoughts with the designers. 
            </p><p>
                In June 2011, JannPaul added the Cushion Brellia, the World's first Cushion Hearts & Arrows diamond to their collection. It was the most brilliant cushion diamond in the world that was exclusively to us. By 2013, we tapped into the modified round diamonds market and brought in the Solasfera, an extraordinary 10 Hearts & Arrows diamond. The Solasfera is the only modified round diamond that is known to achieve the maximum brilliance, fire and scintillation result in the Gemex Light Performance report. Recently, in 2015, we designed and introduced the World's first Octagon 88 faceted Hearts & Arrows diamond. 
            </p><p>
                The vision of the company is to educate the public and empower them with knowledge to make an informed decision. Our goal is to offer the best diamond cuts in all shapes, at the most competitive price. By doing this, we hope to revolutionize the diamond industry and raise the benchmark.
            </p>
        <?php } else if (filter_input(INPUT_GET, "page") == "3") { ?>
            <h3>FUTURE</h3>
            <img class="fill" src="../../dist/img/future.png"/>
            <p>
                JannPaul utilizes the latest technology and equipments to improve their diamonds and craftsmanship. We are constantly researching new methods to allow consumers to easily analyze a diamond's quality. We continue to develop new fancy shape diamonds cuts with Hearts & Arrows. Our aim is to offer the most brilliant diamond cut in all diamond shapes. 
            </p><p>
                In 2016, JannPaul will open its new flagship store in Orchard road, besides House of Hung. This will be the same year where our official e-commerce store will be launched. Our diamond educational articles and videos will be easily accessible on our website, which will be one of the most diamond informational site. Our website will include comprehensive diamonds scopes images (Actual, Ideal, ASET and Hearts & Arrows) to allow consumers to have full information on their purchases. 
            </p><p>
                In the future, we plan to strengthen our international presence and distribute our patented diamond cuts worldwide.  
            </p>
        <?php } ?>
    </body>
</html>