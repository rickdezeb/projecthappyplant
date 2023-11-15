function message() {
    var Name = document.getElementById('name');
    var email = document.getElementById('email');
    var msg = document.getElementById('msg');
    const success = document.getElementById('success');
    const danger = document.getElementById('danger');
  
    if (Name.value === '' || email.value === '' || msg.value === '') {
      danger.style.display = 'block';
      success.style.display = 'none'; // Ensure success message is hidden if there's an error
    } else {
      setTimeout(() => {
        Name.value = '';
        email.value = '';
        msg.value = '';
        success.style.display = 'block';
        danger.style.display = 'none'; // Hide the danger message after a successful submission
      }, 500);
    }

    setTimeout(() => {
        danger.style.display = 'none';
        success.style.display = 'none';
    }, 4000);
  }
  