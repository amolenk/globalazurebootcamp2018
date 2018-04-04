FROM microsoft/wcf:4.6.2

# Create a directory for the application
WORKDIR EmployeeService

# configure the new site in IIS.
RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "EmployeeService" -PhysicalPath C:\EmployeeService -BindingInformation "*:83:"

# This instruction tells the container to listen on port 83. 
EXPOSE 83

# The final instruction copies the published site into the container.
COPY EmployeeService/ .