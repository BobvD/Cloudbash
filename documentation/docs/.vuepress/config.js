module.exports = {
  title: 'Cloudbash',
  base: '/Cloudbash/',
  description: 'Event Sourced Serverless Architecture',
  themeConfig: {
    logo: 'https://cloudbash-frontend.s3.amazonaws.com/cloudbash_rgb.svg',
    // lastUpdated: 'Last updated',
    repo: 'https://github.com/BobvD/Cloudbash',
    // docsDir: 'docs',
    // editLinks: true,
    // editLinkText: 'Recommend a change',
    displayAllHeaders: true, 
    sidebar: [
      {
        title: 'Introduction',   // required
        path: '/introduction/',      // optional, which should be a absolute path.
        collapsable: false, // optional, defaults to true
      }, 
      {
        title: 'Event Sourcing & CQRS',
        children: [
          ['/event_sourcing/', 'Introduction'],
          ['/serverless_architecture/', 'ES Serverless Architecture'],
        ],
        collapsable: false,
        sidebarDepth: 0,   
      },  
      {
        title: 'Code Examples',
        children: [
          ['/code_examples/mediatr.md', 'Mediator Pattern'],        
        ],
        collapsable: false,
        sidebarDepth: 0,   
      }, 
      {
        title: 'Business Scenario and Analysis',
        path: '/business_scenario/',
        collapsable: false
      },  
      {
        title: 'Serverless Web development',
        children: [
          ['/aws_serverless/serverless_functions.md', 'Restful API'],
          ['/aws_serverless/static_website_hosting.md', 'Static Website Hosting'],
          ['/aws_serverless/s3_file_uploads.md', 'S3 File Uploads'],
          ['/aws_serverless/cognito_auth.md', 'Access Control with Cognito'],
          ['/aws_serverless/data_lake.md', 'Data Lake with S3, Glue & Athena']             
        ],
        collapsable: false,
        sidebarDepth: 0,   
      }, 
      {
        title: 'Dev Ops',
        path: '/dev_ops/'
      }
    ],
    nav: [
      {
        text: 'Home',
        link: 'https://d2bgpsr44efzwn.cloudfront.net/'
      },
      {
        text: 'Live Example',
        link: 'https://github.com/BobvD/Cloudbash'
      }
    ],   
    plugins: [
      '@vuepress/active-header-links',
      'vuepress-plugin-table-of-contents',
      [
        'vuepress-plugin-medium-zoom',
        {
          selector: '.my-wrapper .my-img',
          delay: 1000,
          options: {
            margin: 24,
            background: '#BADA55',
            scrollOffset: 0,
          },
        },
      ]
    ]
  }
}