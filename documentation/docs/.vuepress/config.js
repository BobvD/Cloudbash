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
          ['/introduction/ES', 'Event Sourcing & CQRS'],
          ['/introduction/DDD', 'Domain Driven Design']
        ]
      },      
      {
        title: 'Business Scenario and Analysis',
        path: '/cloudbash/',
        collapsable: false
      },
      {
        title: 'Serverless Architecture',
        children: [ /* ... */ ]
      },
      {
        title: 'Amazon Web Services',
        children: [ /* ... */ ]
      },
      {
        title: 'Dev Ops',
        children: [ /* ... */ ]
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
    plugins: ['@vuepress/active-header-links']
  }
}