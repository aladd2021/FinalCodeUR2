#include <VarSpeedServo.h>

#define in1 10
#define in2 11
#define in3 12
#define in4 13
#define magnet 8
#define arm2 6
#define arm1 5

#define interface 8

// Initialize motors
VarSpeedServo baseRotate = VarSpeedServo(interface, in1, in3, in2, in4);
VarSpeedServo armOne;
VarSpeedServo armTwo;

const byte numChars = 32;
char receivedShape[numChars];
char tempShapeStorage[numChars];
char shape[numChars] = {0};

float xDist = 0;
float yDist = 0;
double baseAngle = 0;
double shapeDist = 0;
double pi = 3.14159265358;

double L1 = 2.5; // Height to Link 1 
double L2 = 10; // Length of Link 1
double L3 = 8; // Length of Link 2
double th1 = 0;
double th2 = 0;
double degtoTouch = 0;
double fullStretch = 0;
double x = 0;
double y = 0;

bool continueRotate = true;
bool newShape = false;

void setup(){
  Serial.begin(9600);

  armOne.write(0); 
  armTwo.write(0);

  // Set pins
  armOne.attach(arm1); // set to pin 5
  armTwo.attach(arm2); // set to pin 6
  pinMode(magnet, OUTPUT); // Set pin to 8

  // Initialize movement for base rotation
  baseRotate.setMaxSpeed(1000); 
  baseRotate.setAcceleration(500);
}
void loop(){
  receiveShapeData();
  if (newShape) {
    stopCommunication();
    parseData();
    Kinematics();
    moveRobot();
    delay(1000);
    digitalWrite(magnet, HIGH); // Turn on the magnet
    pickUpPosition();   
      // If the shape is a triangle, return to start angle, rotate 90 degrees left per instructor, drop shape   
      if(shape[0] == 'T')
      {
        baseAngle = -baseAngle;
        baseRotate.setSpeed(300); // need to test speeds
        baseRotate.move(-baseAngle*baseDeg);
        baseRotate.runToPosition();
        delay(1000);
        shapePilePosition();
        Kinematics();
        baseAngle = -90;
        moveRobot();
        delay(1000);
        digitalWrite(magnet, LOW);
        resetPosition();
        moveRobot();
      }
      // Same as above but for Square and 90 degrees right
      else if(shape[0] == 'S')
      {
        baseAngle = -baseAngle;
        baseRotate.setSpeed(300);
        baseRotate.move(-baseAngle*baseDeg);
        baseRotate.runToPosition();
        delay(1000);
        shapePilePosition();
        Kinematics();
        baseAngle = 90;
        moveRobot();
        delay(1000);
        digitalWrite(magnet, LOW);        
        resetPosition();
        moveRobot();
      }
    newShape = false; // tells the Arduino there are no new shapes
    startCommunication();
  }
}
void stopCommunication()
{
  // Send command to C# to stop communication and not allow coordinate sending
  Serial.println("<S1>");
}
void startCommunication() 
{
  // Send command to C# to start communication and allow coordinate sending
  Serial.println("<S0>");
}

void receiveShapeData()
{
  // Store received data from C# so long as it fits the conditions
  static bool receiving = false;
  static byte index = 0;
  char receivedFromPC;

  while (Serial.available() > 0 && newShape == false)
  {
    receivedFromPC = Serial.read();
    if (receiving == true){
      if(receivedFromPC != '>'){
        receivedShape[index] = receivedFromPC;
        index++;
        if(index >= numChars){
          index = numChars - 1;
        }
      }
      else 
      { 
        receivedShape[index] = '\0';
        receiving = false;
        index = 0;
        newShape = true;
      }
    }
    else if (receivedFromPC == '<')
    {
      receiving = true;
    }
  }
  strcpy(tempShapeStorage, receivedShape);
}
void parseData()
{
  char *shapeToken;

  shapeToken = strtok(tempShapeStorage, ",");
  strcpy(shape, shapeToken);

  shapeToken = strtok(NULL, ",");
  xDist = atof(shapeToken);

  shapeToken = strtok(NULL, ",");
  yDist = atof(shapeToken);
}

void Kinematics(){
  baseAngle = radtoDeg(atan(xDist/yDist)); // Rotation angle of the base not sure if needed
  shapeDist = hypot(xDist, yDist); // Distance from the start of the first arm link to the shape
  fullStretch = hypot(L1, shapeDist); // L1 is the height of the first arm link, calculates new shape distance with touch rotation accounted

  // Calculate the angle of the arm links using law of cosines
  th2 = acos((sq(fullStretch)-sq(L2)-sq(L3))/(2.0*L2*L3)); 
  th1 = atan((L3*sin(th2))/(L2+L3*cos(th2)));

  degtoTouch = radtoDeg(atan(L1/fullStretch))-5.0;
}
void moveRobot()
{
  baseRotate.setSpeed(300);
  baseRotate.move(-baseAngle*baseDeg);
  baseRotate.runToPosition();
  armTwo.write(th2, 70, false);
  armOne.write(th1, 50, true);
}
void pickUpPosition()
{
  armOne.write(th1+degtoTouch, 50, true);
  delay(2000);
  armOne.write(30, 50, true);
}

void shapePilePosition()
{
  xDist = 0;
  yDist = 9;
}

void resetPosition(){
  // Reset position of robot
  if(shape[0]=='T'){
    baseAngle = 90;
  }
  else{
    baseAngle = -90;    
  }
  shapeDist = 0;
  fullStretch = 0;
  th1 = 0;
  th2 = 0;
  degtoTouch = 0;
}


double radtoDeg(double rad){
  // Convert values from radians to degrees
  double deg = rad*(180/pi);
  return deg;
}

double degtoRad(double deg){
  // Convert values from degrees to radians
  double rad = deg*(pi/180);
  return rad;  
}
