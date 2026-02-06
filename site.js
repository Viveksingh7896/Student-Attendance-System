// Custom JavaScript for Student Attendance System

// Auto-hide alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert-dismissible');
    
    alerts.forEach(function(alert) {
        setTimeout(function() {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });
});

// Confirm delete actions
function confirmDelete(itemName) {
    return confirm(`Are you sure you want to delete ${itemName}? This action cannot be undone.`);
}

// Mark all students present
function markAllPresent() {
    document.querySelectorAll('.status-select').forEach(select => {
        select.value = '0'; // Present
        updateSelectColor(select);
    });
}

// Mark all students absent
function markAllAbsent() {
    document.querySelectorAll('.status-select').forEach(select => {
        select.value = '1'; // Absent
        updateSelectColor(select);
    });
}

// Update select color based on status
function updateSelectColor(select) {
    select.classList.remove('bg-success', 'bg-danger', 'bg-warning', 'bg-info', 'text-white');
    
    switch(select.value) {
        case '0': // Present
            select.classList.add('bg-success', 'text-white');
            break;
        case '1': // Absent
            select.classList.add('bg-danger', 'text-white');
            break;
        case '2': // Late
            select.classList.add('bg-warning');
            break;
        case '3': // Excused
            select.classList.add('bg-info', 'text-white');
            break;
    }
}

// Form validation helper
function validateAttendanceForm() {
    const classSelect = document.querySelector('select[name="classId"]');
    const dateInput = document.querySelector('input[name="date"]');
    
    if (!classSelect.value) {
        alert('Please select a class');
        return false;
    }
    
    if (!dateInput.value) {
        alert('Please select a date');
        return false;
    }
    
    return true;
}

// Search debounce
let searchTimeout;
function debounceSearch(searchFunction, delay = 500) {
    clearTimeout(searchTimeout);
    searchTimeout = setTimeout(searchFunction, delay);
}

// Export table to CSV
function exportTableToCSV(filename) {
    const table = document.querySelector('table');
    let csv = [];
    const rows = table.querySelectorAll('tr');
    
    for (let i = 0; i < rows.length; i++) {
        const row = [];
        const cols = rows[i].querySelectorAll('td, th');
        
        for (let j = 0; j < cols.length; j++) {
            row.push(cols[j].innerText);
        }
        
        csv.push(row.join(','));
    }
    
    downloadCSV(csv.join('\n'), filename);
}

function downloadCSV(csv, filename) {
    const csvFile = new Blob([csv], { type: 'text/csv' });
    const downloadLink = document.createElement('a');
    downloadLink.download = filename;
    downloadLink.href = window.URL.createObjectURL(csvFile);
    downloadLink.style.display = 'none';
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
}

// Calculate attendance percentage
function calculateAttendancePercentage(present, total) {
    if (total === 0) return 0;
    return Math.round((present / total) * 100);
}

// Highlight low attendance
function highlightLowAttendance() {
    document.querySelectorAll('.attendance-percentage').forEach(element => {
        const percentage = parseFloat(element.textContent);
        if (percentage < 75) {
            element.classList.add('text-danger', 'fw-bold');
        } else if (percentage < 85) {
            element.classList.add('text-warning', 'fw-bold');
        } else {
            element.classList.add('text-success', 'fw-bold');
        }
    });
}

// Initialize tooltips if Bootstrap is available
document.addEventListener('DOMContentLoaded', function() {
    if (typeof bootstrap !== 'undefined') {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }
});
