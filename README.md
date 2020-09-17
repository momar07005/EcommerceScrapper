# EcommerceScrapper

## Features

- As a user I want to access the app using my browser
    - Acceptance criteria
        - Page loads without errors ✅
        - App displays a comprehensive interface✅
- As a user I want to be able to request ASINs indexation by entering ASINs
    - Acceptance criteria
        - App should display the list of ASINs I requested ✅
        - (bonus) bulk add ✅
- As a user I want the app to automatically retrieve top **10 most recent reviews** for products I requested indexation
    - Acceptance criteria
        - A state indicator should indicate which ASINs are loading, indexed or in an error state ✅
        - (bonus) retrieve top 50 most recent reviews per product ✅
- As a user I want to view the list of all extracted reviews as a table
    - Acceptance criteria
        - Expected columns: Product ASIN, Review Date, Review Title, Review Content, Rating ✅
        - Ability to sort per column ❌
        - (bonus) Text search within review texts ✅
        
  ## Bonus
   
    Retrieve any number of reviews (multiple of 10)

    Do not retrieve reviews already indexed
    
    Fast indexation time (Configurable)
    
    
## To Run

  - pre-requisite
  
      Have nodejs, npm, .net core ... installed
      
      
  - Command to run the app : "dotnet run" inside WebUI folder
  
  After that app should be available at http://localhost:5002/
  
  ⚡ : Give time to Angular app to be ready: sometimes the .net core api load before angular app become available
  
  
  
  ## Changes to make for production ready version
  
    - Change database: for this POC, SQLite is embedded.
    - Improve user interface
    - Configure degree of parallelism depending on computer characteristics
    - Make the app asynchronous depending on potential usage
    - Fix reviews' sort by columns feature

## PS:

I do believe that code with well named variables and functions does not need comments !
