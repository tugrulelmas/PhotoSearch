[![Build Status](https://travis-ci.org/tugrulelmas/PhotoSearch.svg?branch=master)](https://travis-ci.org/tugrulelmas/PhotoSearch)

# PhotoSearch
Search photos on [flickr.com](https://www.flickr.com).

# Usage
Run this command `docker run -p 8080:80 tugrulelmas/photo-search -e [flickr-api-key]` and browse the url http://localhost:8080/swagger with your favourite browser.

## Options
`-e flickr-api-key=<flicker api key>`

Your API application key. See [here](https://www.flickr.com/services/api/misc.api_keys.html) for more details.

# Debug
- Simply download or clone this repository.
- Be sure you have [.net sdk](https://www.microsoft.com/net/download) on your local machine
- Go to [src](src) folder.
- Feel free to edit files with your best IDE.
- Run the following commands:
  ```
  dotnet build
  dotnet test
  ```

# License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
