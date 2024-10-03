document.addEventListener('DOMContentLoaded', () => {
  const findCentersBtn = document.getElementById('findCenters');
  const evacuationSection = document.getElementById('evacuationCenters');
  const centerList = document.getElementById('centerList');
  const roomsSection = document.getElementById('roomsSection');
  const roomList = document.getElementById('roomList');
  const roomsMessage = document.getElementById('roomsMessage');
  const selectedCenterName = document.getElementById('selectedCenterName');
  const roomTemplate = document.getElementById('roomTemplate').innerHTML;
  const showForecastBtn = document.getElementById('showForecastBtn');
  const weatherForecastSection = document.getElementById('weatherForecast');
  const forecastDetails = document.getElementById('forecastDetails');
  const showWindyBtn = document.getElementById('showWindyBtn'); 
  const windyMapContainer = document.getElementById('windyMapContainer');
  const logoutBtn = document.getElementById('logoutBtn');
  const readMoreBtn = document.getElementById('readMoreBtn');
  const newsSection = document.getElementById('newsSection');
  const newsList = document.getElementById('newsList');
  const exploreMapBtn = document.getElementById('exploreMapBtn');
  const mapsSection = document.getElementById('mapsSection');
  const mapList = document.getElementById('mapList');
  const watchVideoBtn = document.getElementById('watchVideoBtn');
  const videoSection = document.getElementById('videoSection');
  const scrollToTopBtn = document.getElementById('scrollToTopBtn');
  const browseTipsBtn = document.getElementById('browseTipsBtn');
  const tipsContainer = document.getElementById('tipsContainer');
  const tutorialSection = document.getElementById('tutorialSection');
  const tutorialContainer = document.getElementById('tutorialContainer');
  const homeLink = document.getElementById('homeLink');
  
  homeLink.addEventListener('click', (e) => {
    e.preventDefault(); // Prevent the default link behavior
    document.querySelector('.features').classList.remove('hidden');
    document.querySelector('.hero').classList.remove('hidden');

    document.getElementById('aboutSection').classList.add('hidden');
    document.getElementById('aboutSection').style.display = 'none'; // Add this line
    hideAllSections();
  });

  
  document.getElementById('aboutLink').addEventListener('click', function(e) {
    e.preventDefault();
    document.querySelector('.hero').classList.add('hidden');

    document.querySelector('.features').classList.add('hidden');
    document.getElementById('aboutSection').classList.remove('hidden');
    aboutSection.classList.remove('hidden');
    aboutSection.style.display = 'block';
    aboutSection.scrollIntoView({ behavior: 'smooth' });
    hideAllSections();
  });
  
  // Optional: Add functionality to navigate back to features from the About section


  browseTipsBtn.addEventListener('click', async () => {
    try {
        hideAllSections();
        const response = await fetch('http://localhost:5259/api/Disasters');
        if (!response.ok) {
            throw new Error(`Failed to fetch disasters: ${response.statusText}`);
        }
        const disasters = await response.json();
        displayDisasterButtons(disasters);
        const helpfulTipsSection = document.getElementById('helpfulTipsSection');
        helpfulTipsSection.classList.remove('hidden');
        helpfulTipsSection.style.display = 'block';
        helpfulTipsSection.scrollIntoView({ behavior: 'smooth' });
    } catch (error) {
        console.error('Error fetching disasters:', error);
        alert('Error fetching disasters. Please try again.');
    }
});

function displayDisasterButtons(disasters) {
  tipsContainer.innerHTML = '';
  disasters.forEach(disaster => {
    const button = document.createElement('button');
    button.textContent = disaster.disasterName;
    button.classList.add('disaster-button');

    // Check if the pictureLogoPath is provided and is a valid string
    if (disaster.pictureLogoPath && typeof disaster.pictureLogoPath === 'string') {
      try {
        // Attempt to set the background image
        button.style.backgroundImage = `url('${disaster.pictureLogoPath}')`;
      } catch (error) {
        console.error(`Error setting background image for ${disaster.disasterName}:`, error);
      }
    } else {
      console.warn(`Invalid or missing pictureLogoPath for ${disaster.disasterName}`);
    }

    button.addEventListener('click', () => displayTutorialText(disaster.disasterName));
    tipsContainer.appendChild(button);
  });
}

async function displayTutorialText(disasterName) {
  try {
    const response = await fetch(`http://localhost:5259/api/TutorialText/${disasterName}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch tutorial text: ${response.statusText}`);
    }
    const tutorialText = await response.json();

    // Pass disasterName along with the tutorialText
    showTutorialSection(disasterName, tutorialText);
  } catch (error) {
    console.error('Error fetching tutorial text:', error);
    alert('Error fetching tutorial text. Please try again.');
  }
}

function showTutorialSection(disasterName, tutorialText) {
  if (!tutorialText || !tutorialText.tutorialText) {
    console.error('Invalid tutorial text:', tutorialText);
    alert('Invalid tutorial text. Please try again.');
    return;
  }

  let formattedText = tutorialText.tutorialText;

  // Modify the text using JavaScript string methods
  formattedText = formattedText.replace(/\n/g, '<br>'); // Replace newlines with line breaks
  formattedText = `<h2>Helpful Tips for ${disasterName}</h2>${formattedText}`;

  tutorialContainer.innerHTML = formattedText;
  tutorialSection.classList.remove('hidden');
  tutorialSection.style.display = 'block';
  tutorialSection.scrollIntoView({ behavior: 'smooth' });
}

function hideAllSections() {
    const sections = document.querySelectorAll('.section');
    sections.forEach(section => {
        section.classList.add('hidden');
        section.style.display = 'none';
    });
}
  window.addEventListener('scroll', () => {
    if (window.pageYOffset > 300) {
      scrollToTopBtn.style.display = 'block';
    } else {
      scrollToTopBtn.style.display = 'none';
    }
  });

  scrollToTopBtn.addEventListener('click', () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  });

    
  watchVideoBtn.addEventListener('click', async () => {
    try {
      hideAllSections();
      const response = await fetch('http://localhost:5259/api/Videos');
      if (!response.ok) throw new Error('Network response was not ok');
      const videos = await response.json();
      displayVideos(videos);
      const videoSection = document.getElementById('videoSection');
      videoSection.classList.remove('hidden');
      videoSection.classList.add('animate__fadeInUp');
      videoSection.style.display = 'block';
      videoSection.scrollIntoView({ behavior: 'smooth' });
    } catch (error) {
      console.error('Error fetching videos:', error);
      const videoContainer = document.getElementById('videoContainer');
      videoContainer.innerHTML = `<p>Error: Unable to fetch videos. Please try again.</p>`;
    }
  });
  function displayVideos(videos) {
    const videoContainer = document.getElementById('videoContainer');
    videoContainer.innerHTML = videos.map(video => `
      <div class="video-card">
        <h3>${video.title}</h3>
        <iframe width="100%" height="315" src="${video.videoURL}" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
      </div>
    `).join('');
  }


  readMoreBtn.addEventListener('click', async () => {
    try {
      hideAllSections() 
      console.log('Read More button clicked');
        const response = await fetch('http://localhost:5259/api/NewsEvents');
        if (!response.ok) throw new Error('Network response was not ok');
        const newsEvents = await response.json();
        displayNewsEvents(newsEvents);
        newsSection.classList.remove('hidden');
        newsSection.style.display = 'block';
        newsSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
    } catch (error) {
       console.error('Error fetching news events:', error);
    if (error instanceof TypeError) {
        console.error('Network error. API might be unavailable.');
    } else {
        console.error('API error response:', error.message);
    }
    newsList.innerHTML = `<p>Error: Unable to fetch news events. Please try again.</p>`;
    }
});
function displayNewsEvents(newsEvents) {
        newsList.innerHTML = newsEvents.map(event => `
        <div class="news-card">
        <div class="news-image" style="background-image: url('${event.imagePath}');"></div>
        <div class="news-content">
          <h3>${event.title}</h3>
          <p>Date: ${event.date}</p>
          <p>${event.description}</p>
          <p>By: ${event.by}</p>
        </div>
      </div>
    `).join('');
    const newsImages = document.querySelectorAll('.news-image');
    newsImages.forEach(imgElement => {
      const imagePath = imgElement.style.backgroundImage.slice(5, -2);
      console.log('Image path:', imagePath);
      
      // Add click event listener to each image
      imgElement.addEventListener('click', () => {
        openSecondModal(imagePath);
      });
    });
    }
    exploreMapBtn.addEventListener('click', async () => {
      try {
        hideAllSections() 
        console.log('explore button clicked');
          const response = await fetch('http://localhost:5259/api/HazardMap');
          if (!response.ok) throw new Error('Network response was not ok');
          const HazardMap = await response.json();
          displayHazardMaps(HazardMap);
          mapsSection.classList.remove('hidden');
          mapsSection.style.display = 'block';
          mapsSection.scrollIntoView({ behavior: 'smooth' });
      } catch (error) {
          console.error('Error fetching Maps:', error);
          mapList.innerHTML = `<p>Error: Unable to fetch Maps. Please try again.</p>`;
      }
  });
  function displayHazardMaps(HazardMap) {
          mapList.innerHTML = HazardMap.map(map => `
          <div class="maps-card">
          <div class="maps-image" style="background-image: url('${map.imagePath}');"></div>
          <div class="maps-content">
            <h3>${map.mapName}</h3>
          </div>
        </div>
      `).join('');
      // Add this code to set the background image in CSS
    // Add this code to set the background image in CSS and add event listeners
    const mapsImages = document.querySelectorAll('.maps-image');
    mapsImages.forEach(imgElement => {
        const imagePath = imgElement.style.backgroundImage.slice(5, -2);
        console.log('Image path:', imagePath);
        
        // Add click event listener to each image
        imgElement.addEventListener('click', () => {
            openModal(imagePath);
        });
    });
    
      }
      // Function to open the modal
function openModal(imagePath) {
  const modal = document.getElementById('imageModal');
  const fullImage = document.getElementById('fullImage');
  modal.style.display = "block";
  fullImage.src = imagePath;
}
function openSecondModal(imagePath) {
  const modal = document.getElementById('imageModal2');
  const fullImage = document.getElementById('fullImage2');
  modal.style.display = "block";
  fullImage.src = imagePath;
}

// Get the modal and the close button element
const modal = document.getElementById('imageModal');
const span = document.querySelector('#imageModal .close');

// When the user clicks on the close button, close the modal
span.onclick = function() {
  modal.style.display = "none";
}
// Get the modal and the close button element for the second modal
const modal2 = document.getElementById('imageModal2');
const span2 = document.querySelector('#imageModal2 .close');

// When the user clicks on the close button for the second modal, close the modal
span2.onclick = function() {
  modal2.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  } else if (event.target == modal2) {
    modal2.style.display = "none";
  }
}
  

  if (logoutBtn) {
    logoutBtn.addEventListener('click', async (e) => {
      e.preventDefault();
      try {
        const response = await fetch('/account/logout', { method: 'POST' });
        if (response.ok) {
          window.location.href = '/account/login'; // Redirect to login page
        } else {
          console.error('Logout failed');
          alert('Logout failed. Please try again.');
        }
      } catch (error) {
        console.error('Error during logout:', error);
        alert('An error occurred. Please try again.');
      }
    });
  }
  
  findCentersBtn.addEventListener('click', async () => {
    try {
      hideAllSections();
      //const response = await fetch('http://localhost:5259/api/EvacuationCenter');
        const response = await fetch('http://localhost:5259/api/EvacuationCenter');
      if (!response.ok) throw new Error('Network response was not ok');
      const centers = await response.json();
      displayEvacuationCenters(centers);
      evacuationSection.classList.remove('hidden');
      evacuationSection.style.display = 'block';
      evacuationSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
    } catch (error) {
      console.error('Error fetching centers:', error);
      centerList.innerHTML = `<p>Error: Unable to fetch centers. Please try again.</p>`;
    }
  });
  

  centerList.addEventListener('click', async (e) => {
    const centerCard = e.target.closest('.center-card');
    if (!centerCard) return;

    const centerId = centerCard.dataset.centerId;
    const centerName = centerCard.querySelector('h3').textContent;

    try {
      roomsMessage.textContent = 'Loading rooms...';
    roomList.innerHTML = '';
    selectedCenterName.textContent = centerName;
    roomsSection.classList.remove('hidden');
    roomsSection.classList.add('animate__fadeInUp');

    const response = await fetch(`http://localhost:5259/api/EvacuationCenter/${centerId}/rooms`);
    const data = await response.json(); // Use response.json() instead of response.text()

    if (!response.ok) {
      throw new Error(data.message || 'Failed to fetch rooms');
    }

      (data.rooms); // Assuming the response contains a 'rooms' property
  } catch (error) {
    console.error(`Error fetching rooms for center ${centerId}:`, error);
    roomsMessage.textContent = `Error: ${error.message}. Please try again.`;
  }
  });

  function displayEvacuationCenters(centers) {
    centerList.innerHTML = centers.map(center => `
      <div class="center-card" data-center-id="${center.centerID}">
        <h3>${center.centerName}</h3>
        <p>Location: ${center.location}</p>
        <button>View Rooms</button>
      </div>
    `).join('');
  }
  centerList.addEventListener('click', async (e) => {
    const centerCard = e.target.closest('.center-card');
    if (!centerCard) return;
  
    const centerId = centerCard.dataset.centerId;
  
    try {
      roomsMessage.textContent = 'Loading rooms...';
      roomList.innerHTML = '';
      roomsSection.classList.remove('hidden');
      roomsSection.classList.add('animate__fadeInUp');
  
      const response = await fetch(`http://localhost:5259/api/EvacuationCenter/${centerId}/rooms`);
      const data = await response.json();
  
      if (!response.ok) {
        throw new Error(data.message || 'Failed to fetch rooms');
      }
  
      displayCenterDetails(data);
      displayRooms(data.rooms);
      roomsSection.style.display = 'block';
      roomsSection.scrollIntoView({ behavior: 'smooth' }); // Scroll to rooms section
    } catch (error) {
      console.error(`Error:`, error);
      roomsMessage.textContent = error.message;
    }
  });
  
  function displayCenterDetails(data) {
    const centerDetails = document.getElementById('centerDetails');
    centerDetails.innerHTML = `
      <h2>${data.centerName}</h2>
      <p>Location: ${data.location}</p>
      <button onclick="showLocation('${data.location}')">View Location</button>
    `;
  }
  
  function displayRooms(rooms) {
    if (rooms.length === 0) {
      roomsMessage.textContent = 'No rooms available for this center.';
      return;
    }
  
    roomsMessage.textContent = '';
    roomList.innerHTML = rooms.map(room => `
      <div class="room-card">
        <h4>Room: ${room.roomNumber}</h4>
        <p>Family Capacity: ${room.capacity}</p>
      </div>
    `).join('');
  }
  function hideAllSections() {
    evacuationSection.classList.add('hidden');
    evacuationSection.style.display = 'none';
    roomsSection.classList.add('hidden');
    roomsSection.style.display = 'none';
    weatherForecastSection.classList.add('hidden');
    weatherForecastSection.style.display = 'none';
    document.getElementById('windySection').classList.add('hidden');
    document.getElementById('windySection').style.display = 'none';
    newsSection.classList.add('hidden');
    newsSection.style.display = 'none';
    mapsSection.classList.add('hidden');
    mapsSection.style.display = 'none';
    videoSection.classList.add('hidden');
    videoSection.style.display = 'none';
    const helpfulTipsSection = document.getElementById('helpfulTipsSection');
    helpfulTipsSection.classList.add('hidden');
    helpfulTipsSection.style.display = 'none';

    tutorialSection.classList.add('hidden');
    tutorialSection.style.display = 'none';
    

  }
  function showLocation(locationName) {
    // You could open a modal or navigate to a map view
    alert(`Opening map view for: ${locationName}`);
    // In a real app, you'd use a mapping service like Google Maps or OpenStreetMap
  }

  function displayRooms(rooms) {
    if (!Array.isArray(rooms) || rooms.length === 0) {
      roomsMessage.textContent = 'No rooms available for this center.';
      return;
    }

    roomsMessage.textContent = '';
    roomList.innerHTML = rooms.map(room => 
      roomTemplate
        .replace('{roomNumber}', room.roomNumber || 'N/A')
        .replace('{capacity}', room.capacity || 'Unknown')
    ).join('');
  }
  async function GetWeatherForecastAsync(cityName) {
    try {
      const response = await fetch(`https://api.openweathermap.org/data/2.5/weather?q=${cityName}&appid=3c1b42054e212a0088cda5f59832df02&units=metric`);
      console.log('Response status:', response.status);

      if (!response.ok) {
        throw new Error(`HTTP error ${response.status}`);
      }

      const data = await response.json();
      console.log('Response data:', data);

      if (data.cod !== 200) {
        throw new Error(`API error ${data.cod}: ${data.message}`);
      }

      const weatherIcon = `http://openweathermap.org/img/wn/${data.weather[0].icon}@2x.png`;

        // Update UI with weather data
        forecastDetails.innerHTML = `
            <h3>Current Weather in ${data.name}</h3>
            <img src="${weatherIcon}" alt="Weather Icon" class="weather-icon">
            <p>Temperature: ${data.main.temp}°C</p>
            <p>Conditions: ${data.weather[0].description}</p>
            <p>Humidity: ${data.main.humidity}%</p>
            <p>Wind Speed: ${data.wind.speed} m/s</p>
        `;
    } catch (error) {
      console.error('Error fetching weather forecast:', error);
      forecastDetails.innerHTML = `<p>Error: ${error.message}</p>`;
    }
  }
  showWindyBtn.addEventListener('click', () => {
    loadWindyMap();
  });
  function loadWindyMap() {
    const windyMapContainer = document.getElementById('windyMapContainer');
  if (windyMapContainer) {
    windyMapContainer.innerHTML = `
      <iframe 
        width="100%" 
        height="450" 
        src="https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=default&metricTemp=default&metricWind=default&zoom=11&overlay=wind&product=ecmwf&level=surface&lat=13.298&lon=123.731&detailLat=13.358&detailLon=123.731&detail=true&pressure=true" 
        frameborder="0">
      </iframe>
    `;
    const closeWindyBtn = document.getElementById('closeWindyBtn');
    closeWindyBtn.addEventListener('click', closeWindyMap);
  } else {
    console.error('Error: windyMapContainer not found');
  }
  }
  function closeWindyMap() {
    const windyMapContainer = document.getElementById('windyMapContainer');
    const windySection = document.getElementById('windySection');
    
    if (windyMapContainer && windySection) {
      windyMapContainer.innerHTML = ''; // Clear the container
      windySection.classList.add('hidden');
      windySection.style.display = 'none';
      
      // Show the forecast section again
      const weatherForecastSection = document.getElementById('weatherForecast');
      if (weatherForecastSection) {
        weatherForecastSection.classList.remove('hidden');
        weatherForecastSection.style.display = 'block';
        weatherForecastSection.scrollIntoView({ behavior: 'smooth' });
      }
    }
  }
  showForecastBtn.addEventListener('click', async () => {
    try {
      hideAllSections();
      await GetWeatherForecastAsync("Tayhi, Tabaco, Albay, Philippines");
      weatherForecastSection.classList.remove('hidden');
      weatherForecastSection.classList.add('animate__fadeInUp');
      weatherForecastSection.style.display = 'block';
      weatherForecastSection.scrollIntoView({ behavior: 'smooth' });

      const windySection = document.getElementById('windySection');
      // Changed from windyContainer to windyMapContainer to match the HTML
      
      windySection.classList.remove('hidden');
      windySection.classList.add('animate__fadeInUp');
      windySection.style.display = 'block';

      // Instead of trying to embed the Windy.com map, we'll provide a link
     // Clear the Windy map container
    const windyMapContainer = document.getElementById('windyMapContainer');
    if (windyMapContainer) {
      windyMapContainer.innerHTML = '';
    }
  } catch (error) {
    console.error('Error fetching weather forecast:', error);
    forecastDetails.innerHTML = `<p>Error: ${error.message}</p>`;
  }
  });
  hideAllSections()
});
