using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace Day1
{// Apply SOLID design principles on the following code samples for better design
 //1.  
    public class UserService
    {
        public void Register(string email, string password)
        {
            if (!ValidateEmail(email))
                throw new ValidationException("Email is not an email");

            var user = new User(email, password);

            SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });
        }
        public virtual bool ValidateEmail(string email)
        {
            return email.Contains("@");
        }
        public bool SendEmail(MailMessage message)
        {
            _smtpClient.Send(message);
        }
    }

    #region Refactored Code

    public interface ISender
    {
        void SendEmail(MailMessage message);
    }

    public class EmailSender : ISender
    {
        private SmtpClient _smtpClient = new();
        public void SendEmail(MailMessage message)
        {
            _smtpClient.Send(message);
        }
    }

    public interface Ivalidator
    {
        bool Validate(string email);
    }

    public class EmailValidator : Ivalidator
    {
        public bool Validate(string email)
        {
            return email.Contains("@");
        }
    }
    public class UserSevice
    {
        private readonly ISender _emailSender;
        private readonly Ivalidator _emailValidator;
        public UserSevice(IEmailSender emailSender, IEmailValidator validator)
        {
            _emailSender = emailSender;
            _validator = validator;
        }
        public void register(string email, string pass)
        {
            if (!_validator.Validate(email))
                throw new ValidationException("Email is not valid");

            var user = new User(email, password);

            _emailSender.SendEmail(new MailMessage("mysite@nowhere.com", email)
            {
                Subject = "Hello foo"
            });
        }
    }

    #endregion


    // 2.
    //a. Add Square & Triangle & Cube
    //b. Add function to get volume for the supported shapes
    //c. noting that cube shape only support volume calculation

    public class Rectangle
    {
        public double Height { get; set; }
        public double Wight { get; set; }
    }
    public class Circle
    {
        public double Radius { get; set; }
    }
    public class AreaCalculator
    {
        public double TotalArea(object[] arrObjects)
        {
            double area = 0;
            Rectangle objRectangle;
            Circle objCircle;
            foreach (var obj in arrObjects)
            {
                if (obj is Rectangle)
                {
                    area += obj.Height * obj.Width;
                }
                else
                {
                    objCircle = (Circle)obj;
                    area += objCircle.Radius * objCircle.Radius * Math.PI;
                }
            }
            return area;
        }
    }

    #region Refactored Code 
    public interface IShape
    {
        double GetArea();
    }

    public interface I3DShape
    {
        double GetVolume();
    }

    public class Rectangle : IShape
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double GetArea() => Height * Width;
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }
        public double GetArea() => Math.PI * Radius * Radius;
    }

    public class Triangle : IShape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public double GetArea() => 0.5 * Base * Height;
    }

    public class Cube : IShape, I3DShape
    {
        public double Side { get; set; }
        public double GetArea() => 6 * Side * Side;
        public double GetVolume() => Side * Side * Side;
    }

    public class AreaCalculator
    {
        public double TotalArea(IEnumerable<IShape> shapes) => shapes.Sum(s => s.GetArea());
    }

    public class VolumeCalculator
    {
        public double TotalVolume(IEnumerable<I3DShape> shapes) => shapes.Sum(s => s.GetVolume());
    }

    #endregion

    // 3.
    class Rectangle
 def initialize(width, height)
 @width, @height = width, height
 end
 def set_width(width)
 @width = width
 end
 def set_height(height)
 @height = height
 end
end
class Square<Rectangle "inherits"
 def set_width(width)
 super(width)
 @height = height
 end
 def set_height(height)
 super(height)
 @width = width
 end
end

    #region Refactored Code 
 class Shape
  def area
    raise NotImplementedError, "Implement in subclass"
  end
end

class Rectangle<Shape
  def initialize(width, height)
    @width = width
    @height = height
  end

  def area
    @width* @height
  end
end

class Square<Shape
  def initialize(side)
    @side = side
  end

  def area
    @side* @side
  end
end

#endregion
}
