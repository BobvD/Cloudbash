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
        sidebarDepth: 1,    // optional, defaults to 1
        children: [
          ['/introduction/DDD', 'Getting started'],
          ['/introduction/ES', 'Technical stack']          
        ]
      }, 
      {
        title: 'Event Sourcing & CQRS',
        children: [
          ['/event_sourcing/', 'Introductie'],
          ['/introduction/ES', 'Implementatie']          
        ],
        sidebarDepth: 1,   
      },   
      {
        title: 'Domain Driven Design',
        path: '/domain_driven_design/',
      },     
      {
        title: 'Business Scenario and Analysis',
        path: '/business_scenario/',
        collapsable: false
      },      
      {
        title: 'Serverless Architecture',
        path: '/serverless_architecture/'
      },
      {
        title: 'Amazon Web Services',
        path: '/aws/'
      },
      {
        title: 'Dev Ops',
        path: '/dev_ops/'
      }
    ],
    nav: [
      {
        text: 'Home',
        link: '/'
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